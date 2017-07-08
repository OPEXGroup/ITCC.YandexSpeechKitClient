// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeechKitClient.Attributes;

namespace ITCC.YandexSpeechKitClient.Enums
{
    /// <summary>
    /// Format of synthesized audio.
    /// </summary>
    public enum SynthesisAudioFormat
    {
        /// <summary>
        /// Audio in MPEG format, MPEG-1 Audio Layer 3 media container.
        /// </summary>
        [EnumValueString("mp3")]
        Mp3,

        /// <summary>
        /// Audio in PCM 16-bit format, WAV media container.
        /// </summary>
        [EnumValueString("wav")]
        Wav,

        /// <summary>
        /// Audio in Opus format, using OGG as a container.
        /// </summary>
        [EnumValueString("opus")]
        Opus
    }
}