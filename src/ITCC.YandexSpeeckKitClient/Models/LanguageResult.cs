// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeeckKitClient.Enums;

namespace ITCC.YandexSpeeckKitClient.Models
{
    /// <summary>
    /// Language biometric reuslt.
    /// </summary>
    public class LanguageResult : BaseResultModel
    {
        /// <summary>
        /// Language hypothesis.
        /// </summary>
        public DetectedLanguage Language { get; }

        /// <param name="confidence">Must be in range from 0 to 1.</param>
        /// <param name="detectedLanguage">Speaker language value.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal LanguageResult(float confidence, DetectedLanguage detectedLanguage) : base(confidence)
        {
            Language = detectedLanguage;
        }
    }
}
