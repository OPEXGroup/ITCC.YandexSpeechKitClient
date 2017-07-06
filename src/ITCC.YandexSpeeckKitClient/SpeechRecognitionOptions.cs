// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Enums;

namespace ITCC.YandexSpeeckKitClient
{
    /// <summary>
    /// Speech recognition options
    /// </summary>
    public class SpeechRecognitionOptions
    {
        /// <summary>
        /// The language model to use for recognition.
        /// </summary>
        public SpeechModel SpeechModel { get; }

        /// <summary>
        /// The audio format.
        /// </summary>
        public RecognitionAudioFormat AudioFormat { get; }

        /// <summary>
        /// The language for speech recognition.
        /// </summary>
        public RecognitionLanguage Language { get; }

        /// <summary>
        /// Create new speech recognition options.
        /// </summary>
        /// <param name="speechModel">The language model to use for recognition.</param>
        /// <param name="audioFormat">The audio format.</param>
        /// <param name="language">The language for speech recognition.</param>
        public SpeechRecognitionOptions(SpeechModel speechModel, RecognitionAudioFormat audioFormat, RecognitionLanguage language)
        {
            SpeechModel = speechModel;
            AudioFormat = audioFormat;
            Language = language;
        }
    }
}
