// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
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
    /// <summary>
    /// Session used for speech recognition in data streaming mode.
    /// </summary>
    public class SpeechRecognitionSession : IDisposable
    {
        #region Private fields

        private readonly TcpClient _tcpClient;
        private Stream _newtworkStream;
        private readonly string _applicationName;
        private readonly string _apiKey;

        #endregion

        #region Public properties

        /// <summary>
        /// Connection security options.
        /// </summary>
        public ConnectionMode ConnectionMode { get; }

        /// <summary>
        /// The language model to use for recognition.
        /// </summary>
        public SpeechModel SpeechModel { get; }

        /// <summary>
        /// The audio format.
        /// </summary>
        public RecognitionAudioFormat AudioFormat { get; }

        /// <summary>
        /// The language for speech recognition.
        /// </summary>
        public RecognitionLanguage Language { get; }

        /// <summary>
        /// Biometric parameters to analyse.
        /// </summary>
        public BiometryParameters BiometryParameters { get; }

        /// <summary>
        /// Coordinates of device.
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// User's universally unique identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// The type of device running the client application.
        /// </summary>
        public string Device { get; }

        /// <summary>
        /// The session ID. Specify this ID when contacting tech support.
        /// </summary>
        public string SessionId { get; private set; }

        #endregion

        #region Constructors

        internal SpeechRecognitionSession(
            string applicationName,
            string apiKey,
            Guid userId,
            string device,
            ConnectionMode connectionMode,
            SpeechRecognitionSessionOptions options,
            int timeout)
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

            _tcpClient = new TcpClient
            {
                Client =
                {
                    SendTimeout = timeout,
                    ReceiveTimeout = timeout
                }
            };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Start new speech recognition session. If connection fails or server response code if rather than 200 session will be disposed.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns></returns>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<StartSessionResult> StartAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();

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
            catch (IOException ioException) when (ioException.InnerException is SocketException socketException && socketException.SocketErrorCode == SocketError.TimedOut)
            {
                return StartSessionResult.TimedOut;
            }
            catch (SocketException socketException) when (socketException.SocketErrorCode == SocketError.TimedOut)
            {
                return StartSessionResult.TimedOut;
            }
            catch (SocketException socketException)
            {
                return new StartSessionResult(socketException.SocketErrorCode, socketException.Message);
            }
        }

        /// <summary>
        /// Send new chunk ou audio data to recognize.
        /// </summary>
        /// <param name="data">Binary audio data.</param>
        /// <param name="lastChunk">Indicates this chunk is the last chunk in current session. If true server forms final results and closes connection after next result request.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<SendChunkResult> SendChunkAsync(byte[] data, bool lastChunk = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (data.Length > 1024 * 1024)
                throw new ArgumentOutOfRangeException(nameof(data), data.Length, "Chunk size must be less than 1 MB.");

            ThrowIfDisposed();

            var message = new AddDataMessage
            {
                AudioData = data,
                LastChunk = lastChunk
            };

            try
            {
                await _newtworkStream.SendMessageAsync(message, cancellationToken).ConfigureAwait(false);
                return SendChunkResult.OkResult;
            }
            catch (IOException ioException) when (ioException.InnerException is SocketException socketException && socketException.SocketErrorCode == SocketError.TimedOut)
            {
                return SendChunkResult.TimedOut;
            }
            catch (SocketException socketException) when (socketException.SocketErrorCode == SocketError.TimedOut)
            {
                return SendChunkResult.TimedOut;
            }
            catch (SocketException socketException)
            {
                return new SendChunkResult(socketException.SocketErrorCode);
            }
        }

        /// <summary>
        /// Receive recognition results of previously uploaded chunks.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns></returns>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<ChunkRecognitionResult> ReceiveRecognitionResultAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();

            try
            {
                var response = await _newtworkStream.GetDeserializedMessageAsync<AddDataResponseMessage>(cancellationToken).ConfigureAwait(false);
                return new ChunkRecognitionResult(response);
            }
            catch (IOException ioException) when (ioException.InnerException is SocketException socketException && socketException.SocketErrorCode == SocketError.TimedOut)
            {
                return ChunkRecognitionResult.TimedOut;
            }
            catch (SocketException socketException) when (socketException.SocketErrorCode == SocketError.TimedOut)
            {
                return ChunkRecognitionResult.TimedOut;
            }
            catch (SocketException socketException)
            {
                return new ChunkRecognitionResult(socketException.SocketErrorCode);
            }
        }

        #endregion

        #region Private methods

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
            SpeechkitVersion = Configuration.SpeechkitVersion,
            ApiKey = _apiKey,
            ServiceName = Configuration.SpeechkitServiceName,
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

            await _newtworkStream.WriteAsync(requestMessage, 0, requestMessage.Length, cancellationToken).ConfigureAwait(false);
            var responseBytes = await _newtworkStream.ReceiveAllBytesAsync(cancellationToken).ConfigureAwait(false);

            return Encoding.UTF8.GetString(responseBytes);
        }

        #endregion

        #region IDisposable

        private bool _disposed;

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
                return;

#if NETSTANDARD1_3
            _tcpClient?.Dispose();
#elif NET45 || NET46
            _tcpClient?.Close();
#endif

            _disposed = true;
        }
        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(SpeechRecognitionSession));
        }

        #endregion
    }
}
