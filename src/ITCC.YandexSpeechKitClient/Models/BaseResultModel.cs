// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace ITCC.YandexSpeechKitClient.Models
{
    /// <summary>
    /// Base biometric result type.
    /// </summary>
    public abstract class BaseResultModel
    {
        /// <summary>
        /// Hypothesis confidence.
        /// </summary>
        public float Confidence { get; }

        /// <summary>
        /// </summary>
        /// <param name="confidence">Hypothesis confidence.</param>
        protected BaseResultModel(float confidence)
        {
            Confidence = confidence;
        }
    }
}
