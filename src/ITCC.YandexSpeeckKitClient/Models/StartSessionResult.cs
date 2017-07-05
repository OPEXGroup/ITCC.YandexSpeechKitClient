// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Net.Sockets;
using System.Security.Authentication;
using ITCC.YandexSpeeckKitClient.Enums;
using ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode;
using JetBrains.Annotations;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class StartSessionResult
    {
        public TransportStatus TransportStatus { get; }
        public ResponseCode ResponseCode { get; }
        public string ApiErrorMessage { get; }
        public string SessionId { get; }

        public string ServerHelloResponse { get; }

        public SocketError? SocketError { get; }
        public string TransportErrorMessage { get; }


        internal StartSessionResult([NotNull] ConnectionResponseMessage connectionResponseMessage)
        {
            if (connectionResponseMessage == null)
                throw new ArgumentNullException(nameof(connectionResponseMessage));

            TransportStatus = TransportStatus.Ok;

            ResponseCode = connectionResponseMessage.ResponseCode;
            ApiErrorMessage = connectionResponseMessage.Message;
            SessionId = connectionResponseMessage.SessionId;
        }
        internal StartSessionResult([NotNull] AuthenticationException authenticationException)
        {
            if (authenticationException == null)
                throw new ArgumentNullException(nameof(authenticationException));

            TransportStatus = TransportStatus.CryptographyError;
            TransportErrorMessage = authenticationException.Message;
        }
        internal StartSessionResult([NotNull] SocketException socketException)
        {
            if (socketException == null)
                throw new ArgumentNullException(nameof(socketException));

            TransportStatus = TransportStatus.SocketError;
            SocketError = socketException.SocketErrorCode;
            TransportErrorMessage = socketException.Message;
        }
        internal StartSessionResult(string response)
        {
            TransportStatus = TransportStatus.UnexpectedServerResponse;
            ServerHelloResponse = response;
        }
    }
}
