// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Enums;

namespace ITCC.YandexSpeeckKitClient
{
    public class SpeechRecognitionOptions
    {
        public SpeechModel SpeechModel { get; set; }
        public RecognitionAudioFormat AudioFormat { get; set; }
        public RecognitionLanguage Language { get; set; }
    }
}
