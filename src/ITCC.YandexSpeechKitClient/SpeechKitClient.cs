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
using ITCC.YandexSpeechKitClient.Enums;
using ITCC.YandexSpeechKitClient.Extensions;
using ITCC.YandexSpeechKitClient.Models;
using ITCC.YandexSpeechKitClient.Utils;

namespace ITCC.YandexSpeechKitClient
{
    /// <summary>
    /// Yandex SpeechKit Cloud API client for HTTP operations.
    /// </summary>
    public class SpeechKitClient : IDisposable
    {
        #region Private fields

        private readonly string _apiKey;
        private readonly Guid _userId;
        private readonly HttpClient _httpClient = new HttpClient();

        #endregion

        #region Constructors

        /// <summary>
        /// Create new Yandex SpeechKit Cloud API client.
        /// </summary>
        /// <param name="speechKitClientOptions">API client options..</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public SpeechKitClient(SpeechKitClientOptions speechKitClientOptions)
        {
            if (speechKitClientOptions == null)
                throw new ArgumentNullException(nameof(speechKitClientOptions));

            _apiKey = speechKitClientOptions.ApiKey;
            _userId = speechKitClientOptions.UserId;
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
