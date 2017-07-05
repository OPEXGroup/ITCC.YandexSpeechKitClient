// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITCC.YandexSpeeckKitClient.Enums;
using ITCC.YandexSpeeckKitClient.Extensions;
using ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode;
using ITCC.YandexSpeeckKitClient.Models;
using ITCC.YandexSpeeckKitClient.Utils;

namespace ITCC.YandexSpeeckKitClient
{
    public class SpeechRecognitionSession : IDisposable
    {
        private const string SpeechkitVersion = "";
        private const string SpeechkitServiceName = "asr_dictation";

        private readonly TcpClient _tcpClient;
        private Stream _newtworkStream;
        private readonly string _applicationName;
        private readonly string _apiKey;

        public ConnectionMode ConnectionMode { get; }
        public SpeechModel SpeechModel { get; }
        public RecognitionAudioFormat AudioFormat { get; }
        public RecognitionLanguage Language { get; }
        public BiometryParameters BiometryParameters { get; }
        public Position Position { get; }
        public Guid UserId { get; }
        public string Device { get; }
        public string SessionId { get; private set; }

        internal SpeechRecognitionSession(
            string applicationName,
            string apiKey,
            Guid userId,
            string device,
            ConnectionMode connectionMode,
            SpeechRecognitionSessionOptions options)
        {
            _apiKey = apiKey;
            UserId = userId;
            Device = device;
            ConnectionMode = connectionMode;

            SpeechModel = options.SpeechModel;
            AudioFormat = options.AudioFormat;
            Language = options.Language;
            BiometryParameters = options.BiometryParameters;
            _applicationName = applicationName;
            Position = options.Position;

            _tcpClient = new TcpClient();
        }

        public async Task<StartSessionResult> StartAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _tcpClient.ConnectAsync(Configuration.RecognitionEndpointAddress, GetPort(ConnectionMode));

                switch (ConnectionMode)
                {
                    case ConnectionMode.Secure:
                        _newtworkStream = new SslStream(_tcpClient.GetStream());
                        await ((SslStream)_newtworkStream).AuthenticateAsClientAsync(Configuration
                            .RecognitionEndpointAddress);
                        break;
                    case ConnectionMode.Insecure:
                        _newtworkStream = _tcpClient.GetStream();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var handshakeResponseString = await HandshakeAsync(cancellationToken);
                if (!handshakeResponseString.Contains(Configuration.HelloResponseSuccessTrigger))
                    return new StartSessionResult(handshakeResponseString);

                await _newtworkStream.SendMessageAsync(ConnectionRequestMessage, cancellationToken);
                var connectionResponse =
                    await _newtworkStream.GetDeserializedMessageAsync<ConnectionResponseMessage>(cancellationToken);

                var result = new StartSessionResult(connectionResponse);
                SessionId = result.SessionId;
                return result;
            }
            catch (AuthenticationException authenticationException)
            {
                return new StartSessionResult(authenticationException);
            }
            catch (SocketException socketException)
            {
                return new StartSessionResult(socketException);
            }
            catch (IOException ioException) when (ioException.InnerException is SocketException socketException && socketException.SocketErrorCode == SocketError.TimedOut)
            {
                return new StartSessionResult(socketException);
            }
        }
        public async Task SendChunkAsync(byte[] data, bool lastChunk = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var message = new AddDataMessage
            {
                AudioData = data,
                LastChunk = lastChunk
            };

            await _newtworkStream.SendMessageAsync(message, cancellationToken).ConfigureAwait(false);
        }
        public async Task<ChunkRecognitionResult> GetResponse(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _newtworkStream.GetDeserializedMessageAsync<AddDataResponseMessage>(cancellationToken).ConfigureAwait(false);
            return new ChunkRecognitionResult(response);
        }

        private int GetPort(ConnectionMode connectionMode)
        {
            switch (ConnectionMode)
            {
                case ConnectionMode.Secure:
                    return Configuration.SslPort;
                case ConnectionMode.Insecure:
                    return Configuration.UnsecurePort;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private ConnectionRequestMessage ConnectionRequestMessage => new ConnectionRequestMessage
        {
            ApplicationName = _applicationName,
            SpeechkitVersion = SpeechkitVersion,
            ApiKey = _apiKey,
            ServiceName = SpeechkitServiceName,
            Uuid = UserId.ToUuid(),
            Device = Device,
            Coords = Position.ToString(),
            Topic = SpeechModel.GetEnumString(),
            Lang = Language.GetEnumString(),
            Format = AudioFormat.GetEnumString(),
            AdvancedAsrOptionsMessage = BiometryParameters == BiometryParameters.None
                ? new AdvancedAsrOptionsMessage
                {
                    PartialResults = true
                }
                : new AdvancedAsrOptionsMessage
                {
                    Biometry = BiometryParameters.GetEnumString(),
                    PartialResults = true
                }
        };
        private async Task<string> HandshakeAsync(CancellationToken cancellationToken)
        {
            var requestMessage = Encoding.UTF8.GetBytes(Configuration.HelloMessage);

            await _newtworkStream.WriteAsync(requestMessage, 0, requestMessage.Length, cancellationToken);
            var responseBytes = await ReceiveAsync(_newtworkStream, cancellationToken);

            return Encoding.UTF8.GetString(responseBytes);
        }
        private async Task<byte[]> ReceiveAsync(Stream stream, CancellationToken cancellationToken)
        {
            var sw = new Stopwatch();
            sw.Start();
            using (var ms = new MemoryStream())
            {
                var buffer = new byte[2048];

                while (true)
                {
                    var readCount = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);

                    ms.Write(buffer, 0, readCount);

                    if (readCount < buffer.Length)
                        break;
                }
                sw.Stop();
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        #region IDisposable

        public void Dispose()
        {
#if NETSTANDARD1_3
            _tcpClient?.Dispose();
#elif NET45 || NET46
            _tcpClient?.Close();
#endif
        }

        #endregion
    }
}
