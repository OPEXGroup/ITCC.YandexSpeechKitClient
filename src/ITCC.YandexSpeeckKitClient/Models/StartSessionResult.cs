// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Net.Sockets;
using System.Security.Authentication;
using ITCC.YandexSpeeckKitClient.Enums;
using ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode;

namespace ITCC.YandexSpeeckKitClient.Models
{
    /// <summary>
    /// Start speech recognotion session response.
    /// </summary>
    public class StartSessionResult
    {

        public static StartSessionResult TimedOut { get; } = new StartSessionResult { TransportStatus = TransportStatus.Timeout };

        /// <summary>
        /// 
        /// </summary>
        public TransportStatus TransportStatus { get; private set; }

        /// <summary>
        /// The response code.
        /// </summary>
        public ResponseCode ResponseCode { get; }

        /// <summary>
        /// Error message text. Included in the response if the response code is something other than 200.
        /// </summary>
        public string ApiErrorMessage { get; }

        /// <summary>
        /// The session ID. Specify this ID when contacting tech support.
        /// </summary>
        public string SessionId { get; }

        /// <summary>
        /// Contains server hello response.
        /// </summary>
        public string ServerHelloResponse { get; }

        /// <summary>
        /// Specifies socker error if TransportStatus = SocketError.
        /// </summary>
        public SocketError? SocketError { get; }

        /// <summary>
        /// 
        /// </summary>
        public string TransportErrorMessage { get; }

        private StartSessionResult()
        {
        }
        internal StartSessionResult(ConnectionResponseMessage connectionResponseMessage)
        {
            if (connectionResponseMessage == null)
                throw new ArgumentNullException(nameof(connectionResponseMessage));

            TransportStatus = TransportStatus.Ok;

            ResponseCode = connectionResponseMessage.ResponseCode;
            ApiErrorMessage = connectionResponseMessage.Message;
            SessionId = connectionResponseMessage.SessionId;
        }
        internal StartSessionResult(AuthenticationException authenticationException)
        {
            if (authenticationException == null)
                throw new ArgumentNullException(nameof(authenticationException));

            TransportStatus = TransportStatus.SslNegotiationError;
            TransportErrorMessage = authenticationException.Message;
        }
        internal StartSessionResult(SocketError socketError, string errorMmessage)
        {
            if (socketError == System.Net.Sockets.SocketError.TimedOut)
            {
                TransportStatus = TransportStatus.Timeout;
                return;
            }

            TransportStatus = TransportStatus.SocketError;
            SocketError = socketError;
            TransportErrorMessage = errorMmessage;
        }
        internal StartSessionResult(string response)
        {
            TransportStatus = TransportStatus.UnexpectedServerResponse;
            ServerHelloResponse = response;
        }
    }
}
