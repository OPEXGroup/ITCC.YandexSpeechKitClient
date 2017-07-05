// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.IO;
using System.Net;
using ITCC.YandexSpeeckKitClient.Enums;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class TextToSpechResult : IDisposable
    {
        public TransportStatus TransportStatus { get; internal set; }
        public HttpStatusCode ResponseCode { get; internal set; }
        public Stream Result { get; internal set; }

        public void Dispose() => Result?.Dispose();
    }
}
