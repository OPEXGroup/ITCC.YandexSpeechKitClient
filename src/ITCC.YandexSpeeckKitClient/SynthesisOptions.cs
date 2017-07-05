// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeeckKitClient.Enums;

namespace ITCC.YandexSpeeckKitClient
{
    public class SynthesisOptions
    {
        public string Text { get; }

        public SynthesisAudioFormat AudioFormat { get; set; } = SynthesisAudioFormat.Mp3;

        public SynthesisQuality Quality { get; set; } = SynthesisQuality.High;

        public SynthesisLanguage Language { get; set; } = SynthesisLanguage.Russian;

        public Speaker Speaker { get; set; } = Speaker.Zahar;

        public double Speed { get; set; } = 1.0;

        public Emotion Emotion { get; set; } = Emotion.Neutral;

        public SynthesisOptions(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));

            Text = text;
        }
    }
}
