// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    /// <summary>
    /// Indicators for the group biometric.
    /// </summary>
    [EnumNameString("group")]
    public enum AgeGroup
    {
        /// <summary>
        /// Child (estimated age is under 14 years old).
        /// </summary>
        [EnumValueString("c")]
        Child,

        /// <summary>
        /// Young male (14–20 years old).
        /// </summary>
        [EnumValueString("ym")]
        YoungMale,

        /// <summary>
        /// Young female (14–20 years old).
        /// </summary>
        [EnumValueString("yf")]
        YoungFemale,

        /// <summary>
        /// Adult male (20–55 years old).
        /// </summary>
        [EnumValueString("am")]
        AdultMale,

        /// <summary>
        /// Adult female (20–55 years old).
        /// </summary>
        [EnumValueString("af")]
        AdultFemale,

        /// <summary>
        /// Male over 55.
        /// </summary>
        [EnumValueString("sm")]
        SeniorMale,

        /// <summary>
        /// Female over 55.
        /// </summary>
        [EnumValueString("sf")]
        SeniorFemale
    }
}