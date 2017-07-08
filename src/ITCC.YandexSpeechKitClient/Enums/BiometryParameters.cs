// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeechKitClient.Attributes;

namespace ITCC.YandexSpeechKitClient.Enums
{
    /// <summary>
    /// Biometric analysis parameters.
    /// </summary>
    [Flags]
    public enum BiometryParameters
    {
        /// <summary>
        /// No biometric analysis.
        /// </summary>
        None = 1 << 0,

        /// <summary>
        /// The gender biometric.
        /// </summary>
        [EnumValueString("gender")]
        Gender = 1 << 1,

        /// <summary>
        /// The age group biometric.
        /// </summary>
        [EnumValueString("group")]
        Group = 1 << 2,

        /// <summary>
        /// The language biometric.
        /// </summary>
        [EnumValueString("language")]
        Language = 1 << 3
    }
}