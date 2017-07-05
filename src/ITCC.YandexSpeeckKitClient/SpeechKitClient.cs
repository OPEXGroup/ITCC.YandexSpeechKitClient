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
    public class SpeechKitClient : IDisposable
    {
        private readonly string _apiKey;
        private readonly string _applicationName;
        private readonly Guid _userId;
        private readonly string _device;
        private readonly HttpClient _httpClient = new HttpClient();

        public SpeechKitClient(string apiKey, string applicationName, Guid userId, string device)
        {
            _apiKey = apiKey;
            _applicationName = applicationName;
            _userId = userId;
            _device = device;
        }


        public async Task<SpeechToTextResult> SpeechToTextAsync(SpeechRecognitionOptions options, Stream mediaStream, CancellationToken cancellationToken)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (mediaStream == null)
                throw new ArgumentNullException(nameof(mediaStream));

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
                        Result = new SimpleRecognitionResult(XmlMessageSerializer.DeserializeResponse(contentStream))
                    };
                }
            }
            catch (HttpRequestException)
            {
                return new SpeechToTextResult { TransportStatus = TransportStatus.SocketError };
            }
        }
        public SpeechRecognitionSession CreateSpeechRecognitionSession(ConnectionMode connectionMode, SpeechRecognitionSessionOptions options)
            => new SpeechRecognitionSession(
                _applicationName,
                _apiKey,
                _userId,
                _device,
                connectionMode,
                options);
        public async Task<TextToSpechResult> TextToSpeechAsync(SynthesisOptions options, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

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

        public void Dispose() => _httpClient?.Dispose();
    }
}
