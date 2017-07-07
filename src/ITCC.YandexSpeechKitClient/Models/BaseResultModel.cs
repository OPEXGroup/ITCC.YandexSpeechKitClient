// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace ITCC.YandexSpeeckKitClient.Models
{
    /// <summary>
    /// Base biometric result type.
    /// </summary>
    public abstract class BaseResultModel
    {
        /// <summary>
        /// Confidence of hypothesis.
        /// </summary>
        public float Confidence { get; }

        /// <param name="confidence">Must be in range from 0 to 1.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected BaseResultModel(float confidence)
        {
            if (confidence < 0 || confidence > 1)
                throw new ArgumentOutOfRangeException(nameof(confidence), "Confidence must be in range from 0 to 1.");

            Confidence = confidence;
        }
    }
}
