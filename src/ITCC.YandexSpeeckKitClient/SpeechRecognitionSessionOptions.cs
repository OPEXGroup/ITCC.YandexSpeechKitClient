// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Enums;
using ITCC.YandexSpeeckKitClient.Models;

namespace ITCC.YandexSpeeckKitClient
{
    /// <summary>
    /// Speech recognition session options.
    /// </summary>
    public class SpeechRecognitionSessionOptions
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
        public RecognitionLanguage Language { get; set; } = RecognitionLanguage.Russian;

        /// <summary>
        /// Biometric parameters to analyse.
        /// </summary>
        public BiometryParameters BiometryParameters { get; set; } = BiometryParameters.None;

        /// <summary>
        /// Coordinates of the device running the application.
        /// </summary>
        public Position Position { get; set; } = Position.Zero;

        /// <summary>
        /// Create new speech recognition session options.
        /// </summary>
        /// <param name="speechModel">The language model to use for recognition.</param>
        /// <param name="audioFormat">The audio format.</param>
        public SpeechRecognitionSessionOptions(SpeechModel speechModel, RecognitionAudioFormat audioFormat)
        {
            SpeechModel = speechModel;
            AudioFormat = audioFormat;
        }
    }
}
