// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Net;
using ITCC.YandexSpeechKitClient.Enums;

namespace ITCC.YandexSpeechKitClient.Models
{
    /// <summary>
    /// Result of speech-to-text conversion.
    /// </summary>
    public class SpeechToTextResult
    {
        /// <summary>
        /// Network-level operation result.
        /// </summary>
        public TransportStatus TransportStatus { get; internal set; }

        /// <summary>
        /// Server response code.
        /// </summary>
        public HttpStatusCode StatusCode { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public UtteranceRecognitionResult Result { get; internal set; }
    }
}
