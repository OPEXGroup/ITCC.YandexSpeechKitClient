// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeechKitClient.Attributes;

namespace ITCC.YandexSpeechKitClient.Enums
{
    /// <summary>
    /// The emotional connotation of the voice.
    /// </summary>
    public enum Emotion
    {
        /// <summary>
        /// Cheerful, friendly.
        /// </summary>
        [EnumValueString("good")]
        Good,

        /// <summary>
        /// Neutral (mixed).
        /// </summary>
        [EnumValueString("neutral")]
        Neutral,

        /// <summary>
        /// Irritated.
        /// </summary>
        [EnumValueString("evil")]
        Evil
    }
}