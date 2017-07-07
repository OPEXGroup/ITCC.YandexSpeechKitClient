// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace ITCC.YandexSpeeckKitClient.Enums
{
    /// <summary>
    /// Network-level operation status.
    /// </summary>
    public enum TransportStatus
    {
        /// <summary>
        /// Operation succeeded.
        /// </summary>
        Ok,

        /// <summary>
        /// Operation failed because of unexpected server behaviour.
        /// </summary>
        UnexpectedServerResponse,

        /// <summary>
        /// Operation failed because of SSL/TLS negotiation error.
        /// </summary>
        SslNegotiationError,

        /// <summary>
        /// Operation timed out.
        /// </summary>
        Timeout,

        /// <summary>
        /// Response message stream suddenly ended.
        /// </summary>
        UnexpectedEndOfMessage,

        /// <summary>
        /// Operation failed because of socket error.
        /// </summary>
        SocketError
    }
}