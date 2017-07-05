// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    [EnumNameString("language")]
    public enum DetectedLanguage
    {
        [EnumValueString("de")]
        German,
        [EnumValueString("en")]
        English,
        [EnumValueString("fr")]
        French,
        [EnumValueString("ru")]
        Russian,
        [EnumValueString("tr")]
        Turkish,
        [EnumValueString("uk")]
        Ukrainian
    }
}