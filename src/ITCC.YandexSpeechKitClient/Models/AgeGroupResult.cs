// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeechKitClient.Enums;

namespace ITCC.YandexSpeechKitClient.Models
{
    /// <summary>
    /// Age group biometric reuslt;
    /// </summary>
    public class AgeGroupResult : BaseResultModel
    {
        /// <summary>
        /// Age group hypothesis.
        /// </summary>
        public AgeGroup Group { get; }

        /// <param name="confidence">Must be in range from 0 to 1.</param>
        /// <param name="ageGroup">Speaker age group value.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        internal AgeGroupResult(float confidence, AgeGroup ageGroup) : base(confidence)
        {
            Group = ageGroup;
        }
    }
}
