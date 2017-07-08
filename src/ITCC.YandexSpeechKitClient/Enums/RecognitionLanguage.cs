// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeechKitClient.Attributes;

namespace ITCC.YandexSpeechKitClient.Enums
{
    /// <summary>
    /// The language for speech recognition.
    /// </summary>
    public enum RecognitionLanguage
    {
        /// <summary>
        /// Russian
        /// </summary>
        [EnumValueString("ru-RU")]
        Russian,

        /// <summary>
        /// English
        /// </summary>
        [EnumValueString("en-US")]
        English,

        /// <summary>
        /// Ukrainian
        /// </summary>
        [EnumValueString("uk-UK")]
        Ukrainian,

        /// <summary>
        /// Turkish
        /// </summary>
        [EnumValueString("tr-TR")]
        Turkish,

        /// <summary>
        /// German
        /// </summary>
        [EnumValueString("de-DE")]
        German,

        /// <summary>
        /// Spanish
        /// </summary>
        [EnumValueString("es-ES")]
        Spanish,

        /// <summary>
        /// French
        /// </summary>
        [EnumValueString("fr-FR")]
        French
    }
}