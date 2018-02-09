// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeechKitClient.Enums;

namespace ITCC.YandexSpeechKitClient.Models
{
    /// <summary>
    /// Gender biometric result.
    /// </summary>
    public class GenderResult : BaseResultModel
    {
        /// <summary>
        /// Gender hypothesis.
        /// </summary>
        public Gender Gender { get; }

        /// <param name="confidence">Hypothesis confidence.</param>
        /// <param name="gender">Speaker gender value.</param>
        internal GenderResult(float confidence, Gender gender) : base(confidence)
        {
            Gender = gender;
        }
    }
}
