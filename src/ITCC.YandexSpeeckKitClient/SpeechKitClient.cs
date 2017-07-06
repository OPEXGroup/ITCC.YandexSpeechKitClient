// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ITCC.YandexSpeeckKitClient.Enums;
using ITCC.YandexSpeeckKitClient.Extensions;
using ITCC.YandexSpeeckKitClient.Models;
using ITCC.YandexSpeeckKitClient.Utils;

namespace ITCC.YandexSpeeckKitClient
{
    /// <summary>
    /// Yandex SpeechKit Cloud API client.
    /// </summary>
    public class SpeechKitClient : IDisposable
    {
        #region Private fields

        private readonly string _apiKey;
        private readonly string _applicationName;
        private readonly Guid _userId;
        private readonly string _device;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly int _timeout;

        #endregion

        #region Constructors

        /// <summary>
        /// Create new Yandex SpeechKit Cloud API client.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="applicationName">Name of the client application.</param>
        /// <param name="userId">User's identifier.</param>
        /// <param name="device">The type of device running the client application.</param>
        /// <param name="timeout">Data streaming operation's timeout.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SpeechKitClient(string apiKey, string applicationName, Guid userId, string device, TimeSpan timeout)
            : this(apiKey, applicationName, userId, device, (int) timeout.TotalMilliseconds)
        {
        }

        /// <summary>
        /// Create new Yandex SpeechKit Cloud API client.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="applicationName">Name of the client application.</param>
        /// <param name="userId">User's identifier.</param>
        /// <param name="device">The type of device running the client application.</param>
        /// <param name="timeout">Data streaming operation's timeout in milliseconds.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SpeechKitClient(string apiKey, string applicationName, Guid userId, string device, int timeout = Configuration.DefaultTimeout)
        {
            _apiKey = string.IsNullOrWhiteSpace(apiKey) 
                ? throw new ArgumentNullException(nameof(apiKey)) 
                : apiKey;

            _applicationName = string.IsNullOrWhiteSpace(applicationName) 
                ? throw new ArgumentNullException(nameof(applicationName)) 
                : applicationName;

            _device = string.IsNullOrWhiteSpace(device)
                ? throw new ArgumentNullException(nameof(device))
                : device;

            _userId = userId;
            _timeout = timeout;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Speech-to-text conversion using HTTP mode.
        /// </summary>
        /// <param name="options">Recognition settings.</param>
        /// <param name="mediaStream">Audio stream used for recognition.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<SpeechToTextResult> SpeechToTextAsync(SpeechRecognitionOptions options, Stream mediaStream, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (mediaStream == null)
                throw new ArgumentNullException(nameof(mediaStream));

            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var queryParams = new Dictionary<string, string>
            {
                ["uuid"] = _userId.ToUuid(),
                ["key"] = _apiKey,
                ["topic"] = options.SpeechModel.GetEnumString(),
                ["lang"] = options.Language.GetEnumString()
            };

            var queryString = string.Join("&", queryParams.Select(pair => $"{pair.Key}={pair.Value}"));
            var uri = new Uri($"https://{Configuration.RecognitionEndpointAddress}/asr_xml?{queryString}");

            var message = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StreamContent(mediaStream),
            };

            if (message.Content.Headers.ContentLength > 1024 * 1024)
                throw new ArgumentOutOfRangeException(nameof(mediaStream), message.Content.Headers.ContentLength, "Content length must be less then 1 MB.");

            message.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(options.AudioFormat.GetEnumString());
            message.Headers.TransferEncodingChunked = true;

            try
            {
                var response = await _httpClient.SendAsync(message, cancellationToken).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                    return new SpeechToTextResult
                    {
                        TransportStatus = TransportStatus.Ok,
                        StatusCode = response.StatusCode
                    };

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    return new SpeechToTextResult
                    {
                        TransportStatus = TransportStatus.Ok,
                        StatusCode = response.StatusCode,
                        Result = new UtteranceRecognitionResult(XmlMessageSerializer.DeserializeResponse(contentStream))
                    };
                }
            }
            catch (HttpRequestException)
            {
                return new SpeechToTextResult { TransportStatus = TransportStatus.SocketError };
            }
        }

        /// <summary>
        /// Text-to-speech conversion.
        /// </summary>
        /// <param name="options">Speech synthesis settings.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<TextToSpechResult> TextToSpeechAsync(SynthesisOptions options, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var queryParams = new Dictionary<string, string>
            {
                ["text"] = options.Text,
                ["format"] = options.AudioFormat.GetEnumString(),
                ["lang"] = options.Language.GetEnumString(),
                ["quality"] = options.Quality.GetEnumString(),
                ["speaker"] = options.Speaker.GetEnumString(),
                ["speed"] = options.Speed.ToString("F1").Replace(',', '.'),
                ["emotion"] = options.Emotion.GetEnumString(),
                ["key"] = _apiKey,
            };

            var queryString = string.Join("&", queryParams.Select(pair => $"{pair.Key}={pair.Value}"));
            var uri = new Uri($"{Configuration.SynthesisEndpoint}?{queryString}");
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                try
                {
                    var response = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    if (!response.IsSuccessStatusCode)
                        return new TextToSpechResult
                        {
                            TransportStatus = TransportStatus.Ok,
                            ResponseCode = response.StatusCode
                        };

                    return new TextToSpechResult
                    {
                        TransportStatus = TransportStatus.Ok,
                        ResponseCode = response.StatusCode,
                        Result = await response.Content.ReadAsStreamAsync().ConfigureAwait(false)
                    };
                }
                catch (HttpRequestException)
                {
                    return new TextToSpechResult { TransportStatus = TransportStatus.SocketError };
                }
            }
        }

        /// <summary>
        /// Create new speech recognition session in data streaming mode.
        /// </summary>
        /// <param name="connectionMode">Network security settings.</param>
        /// <param name="options">Recognition settings.</param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public SpeechRecognitionSession CreateSpeechRecognitionSession(ConnectionMode connectionMode, SpeechRecognitionSessionOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            ThrowIfDisposed();

            return new SpeechRecognitionSession(
                _applicationName,
                _apiKey,
                _userId,
                _device,
                connectionMode,
                options,
                _timeout);
        }

        #endregion

        #region IDisposable

        private bool _disposed;

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
                return;

            _httpClient?.Dispose();
            _disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(SpeechKitClient));
        }

        #endregion
    }
}
