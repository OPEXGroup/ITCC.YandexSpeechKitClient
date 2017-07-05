// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    public enum SynthesisAudioFormat
    {
        [EnumValueString("mp3")]
        Mp3,
        [EnumValueString("wav")]
        Wav,
        [EnumValueString("opus")]
        Opus
    }
}