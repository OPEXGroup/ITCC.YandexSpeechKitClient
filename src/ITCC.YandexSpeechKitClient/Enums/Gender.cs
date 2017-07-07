// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    /// <summary>
    /// Indicators for the gender biometric.
    /// </summary>
    [EnumNameString("gender")]
    public enum Gender
    {
        /// <summary>
        /// The speaker is male.
        /// </summary>
        [EnumValueString("male")]
        Male,

        /// <summary>
        /// The speaker is female.
        /// </summary>
        [EnumValueString("female")]
        Female
    }
}