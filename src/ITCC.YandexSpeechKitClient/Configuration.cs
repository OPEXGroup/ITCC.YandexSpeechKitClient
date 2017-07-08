// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace ITCC.YandexSpeechKitClient
{
    internal static class Configuration
    {
        public const string SpeechkitVersion = "";
        public const string SpeechkitServiceName = "asr_dictation";
        public const string RecognitionEndpointAddress = "asr.yandex.net";
        public const string SynthesisEndpoint = "https://tts.voicetech.yandex.net/generate";
        public const int UnsecurePort = 80;
        public const int SslPort = 443;
        public const int DefaultTimeout = 1000;

        public const string HelloMessage = "GET /asr_partial HTTP/1.1\r\nUser-Agent: KeepAliveClient\r\nConnection: keep-alive\r\nHost: asr.yandex.net\r\nUpgrade: dictation\r\n\r\n";
        public const string HelloResponseSuccessTrigger = "HTTP/1.1 101 Switching Protocols";
    }
}
