// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.IO;
using System.Net;
using ITCC.YandexSpeeckKitClient.Enums;

namespace ITCC.YandexSpeeckKitClient.Models
{
    /// <summary>
    /// Result of text-to-speech convertion.
    /// </summary>
    public class TextToSpechResult : IDisposable
    {
        /// <summary>
        /// Network-level operation status.
        /// </summary>
        public TransportStatus TransportStatus { get; internal set; }

        /// <summary>
        /// Server response code.
        /// </summary>
        public HttpStatusCode ResponseCode { get; internal set; }

        /// <summary>
        /// Contains generated audio file stream if ResponseCode = 200.
        /// </summary>
        public Stream Result { get; internal set; }

        /// <inheritdoc />
        public void Dispose() => Result?.Dispose();
    }
}
