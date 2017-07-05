// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Net;
using ITCC.YandexSpeeckKitClient.Enums;
using ITCC.YandexSpeeckKitClient.Models;

namespace ITCC.YandexSpeeckKitClient
{
    public class SpeechToTextResult
    {
        public TransportStatus TransportStatus { get; internal set; }
        public HttpStatusCode StatusCode { get; internal set; }
        public SimpleRecognitionResult Result { get; internal set; }
    }
}
