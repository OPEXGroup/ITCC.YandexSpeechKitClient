// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeechKitClient.Enums;

namespace ITCC.YandexSpeechKitClient.Models
{
    /// <summary>
    /// Language biometric result.
    /// </summary>
    public class LanguageResult : BaseResultModel
    {
        /// <summary>
        /// Language hypothesis.
        /// </summary>
        public DetectedLanguage Language { get; }

        /// <param name="confidence">Confidence of hypothesis.</param>
        /// <param name="detectedLanguage">Speaker language value.</param>
        internal LanguageResult(float confidence, DetectedLanguage detectedLanguage) : base(confidence)
        {
            Language = detectedLanguage;
        }
    }
}
