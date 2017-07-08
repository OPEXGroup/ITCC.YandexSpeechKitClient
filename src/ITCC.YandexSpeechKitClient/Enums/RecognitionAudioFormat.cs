// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeechKitClient.Attributes;

namespace ITCC.YandexSpeechKitClient.Enums
{
    /// <summary>
    /// Format of transmitted audio.
    /// </summary>
    public enum RecognitionAudioFormat
    {
        /// <summary>
        /// The WAV media container can contain any format of audio data (.wav). Recognition begins only after all the data has been transmitted to the server.
        /// </summary>
        [EnumValueString("audio/x-wav")]
        Wav,

        /// <summary>
        /// MPEG-1 Audio Layer 3 (.mp3). Recognition begins only after all the data has been transmitted to the server.
        /// </summary>
        [EnumValueString("audio/x-mpeg-3")]
        Mpeg3,

        /// <summary>
        /// Speex (.ogg, .spx). Make sure that the stream has valid OGG headers. Also make sure that a broadband signal (16000 Hz) is used for encoding.
        /// </summary>
        [EnumValueString("audio/x-speex")]
        Speex,

        /// <summary>
        /// Opus (.ogg, .opus).
        /// </summary>
        [EnumValueString("audio/ogg;codecs=opus")]
        Ogg,

        /// <summary>
        /// Opus (.webm).
        /// </summary>
        [EnumValueString("audio/webm;codecs=opus")]
        WebM,

        /// <summary>
        /// Linear PCM with 16,000 Hz sampling rate and 16-bit quantization (.pcm). Shows the most accurate recognition results.
        /// </summary>
        [EnumValueString("audio/x-pcm;bit=16;rate=16000")]
        Pcm16K,

        /// <summary>
        /// Linear PCM with 8000 Hz sampling rate and 16-bit quantization (.pcm).
        /// </summary>
        [EnumValueString("audio/x-pcm;bit=16;rate=8000")]
        Pcm8K,

        /// <summary>
        /// A-law PCM. 8000 Hz sampling rate and 13-bit quantization (.pcm).
        /// </summary>
        [EnumValueString("audio/x-alaw;bit=13;rate=8000")]
        Alaw
    }
}