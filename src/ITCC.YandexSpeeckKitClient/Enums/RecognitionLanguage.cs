// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    public enum RecognitionLanguage
    {
        [EnumValueString("ru-RU")]
        Russian,
        [EnumValueString("en-US")]
        English,
        [EnumValueString("uk-UK")]
        Ukrainian,
        [EnumValueString("tr-TR")]
        Turkish,
        [EnumValueString("de-DE")]
        German,
        [EnumValueString("es-ES")]
        Spanish,
        [EnumValueString("fr-FR")]
        French
    }
}