// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    /// <summary>
    /// Indicators for the language biometric.
    /// </summary>
    [EnumNameString("language")]
    public enum DetectedLanguage
    {
        /// <summary>
        /// German
        /// </summary>
        [EnumValueString("de")]
        German,

        /// <summary>
        /// English
        /// </summary>
        [EnumValueString("en")]
        English,

        /// <summary>
        /// French
        /// </summary>
        [EnumValueString("fr")]
        French,

        /// <summary>
        /// Russian
        /// </summary>
        [EnumValueString("ru")]
        Russian,

        /// <summary>
        /// Turkish
        /// </summary>
        [EnumValueString("tr")]
        Turkish,

        /// <summary>
        /// Ukrainian
        /// </summary>
        [EnumValueString("uk")]
        Ukrainian
    }
}