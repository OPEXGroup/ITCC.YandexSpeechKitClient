// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    public enum SpeechModel
    {
        [EnumValueString("queries")]
        Queries,
        [EnumValueString("maps")]
        Maps,
        [EnumValueString("dates")]
        Dates,
        [EnumValueString("names")]
        Names,
        [EnumValueString("numbers")]
        Numbers,
        [EnumValueString("music")]
        Music,
        [EnumValueString("buying")]
        Buying
    }
}