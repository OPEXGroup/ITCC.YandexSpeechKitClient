// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    public enum RecognitionAudioFormat
    {
        [EnumValueString("audio/x-wav")]
        Wav,
        [EnumValueString("audio/x-mpeg-3")]
        Mpeg3,
        [EnumValueString("audio/x-speex")]
        Speex,
        [EnumValueString("audio/ogg;codecs=opus")]
        Ogg,
        [EnumValueString("audio/webm;codecs=opus")]
        WebM,
        [EnumValueString("audio/x-pcm;bit=16;rate=16000")]
        Pcm16K,
        [EnumValueString("audio/x-pcm;bit=16;rate=8000")]
        Pcm8K,
        [EnumValueString("audio/x-alaw;bit=13;rate=8000")]
        Alaw
    }
}