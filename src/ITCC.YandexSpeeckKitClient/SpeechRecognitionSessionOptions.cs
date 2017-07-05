// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Enums;
using ITCC.YandexSpeeckKitClient.Models;

namespace ITCC.YandexSpeeckKitClient
{
    public class SpeechRecognitionSessionOptions
    {
        public SpeechModel SpeechModel { get; }
        public RecognitionAudioFormat AudioFormat { get; }

        public RecognitionLanguage Language { get; set; } = RecognitionLanguage.Russian;
        public BiometryParameters BiometryParameters { get; set; } = BiometryParameters.None;
        public Position Position { get; set; } = Position.Zero;

        public SpeechRecognitionSessionOptions(SpeechModel speechModel, RecognitionAudioFormat audioFormat)
        {
            SpeechModel = speechModel;
            AudioFormat = audioFormat;
        }
    }
}
