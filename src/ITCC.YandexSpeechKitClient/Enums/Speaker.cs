// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeechKitClient.Attributes;

namespace ITCC.YandexSpeechKitClient.Enums
{
    /// <summary>
    /// The voice for the synthesized speech.
    /// </summary>
    public enum Speaker
    {
        /// <summary>
        /// Female voice - Jane.
        /// </summary>
        [EnumValueString("jane")]
        Jane,

        /// <summary>
        /// Female voice - Oksana.
        /// </summary>
        [EnumValueString("oksana")]
        Oksana,

        /// <summary>
        /// Female voice - Alyss.
        /// </summary>
        [EnumValueString("alyss")]
        Alyss,

        /// <summary>
        /// Female voice - Omazh.
        /// </summary>
        [EnumValueString("omazh")]
        Omazh,

        /// <summary>
        /// Male voice - Zahar.
        /// </summary>
        [EnumValueString("zahar")]
        Zahar,

        /// <summary>
        /// Male voice - Ermil.
        /// </summary>
        [EnumValueString("ermil")]
        Ermil
    }
}