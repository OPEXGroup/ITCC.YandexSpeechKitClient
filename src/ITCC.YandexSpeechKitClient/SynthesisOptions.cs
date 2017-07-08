// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeechKitClient.Enums;

namespace ITCC.YandexSpeechKitClient
{
    /// <summary>
    /// Speech synthesis options.
    /// </summary>
    public class SynthesisOptions
    {
        /// <summary>
        /// The text to produce speech for. For homographs, use a + before the stressed syllable: def+ect.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// File extension (format) of the synthesized file.
        /// </summary>
        public SynthesisAudioFormat AudioFormat { get; set; } = SynthesisAudioFormat.Mp3;

        /// <summary>
        /// Sampling rate and bit rate of the synthesized PCM audio. Note that the quality parameter only affects the audio characteristics for Wav.
        /// </summary>
        public SynthesisQuality Quality { get; set; } = SynthesisQuality.High;

        /// <summary>
        /// Language
        /// </summary>
        public SynthesisLanguage Language { get; set; } = SynthesisLanguage.Russian;

        /// <summary>
        /// The voice for the synthesized speech.
        /// </summary>
        public Speaker Speaker { get; set; } = Speaker.Zahar;

        /// <summary>
        /// The speed (tempo) of the synthesized speech. 
        /// </summary>
        public double Speed { get; }

        /// <summary>
        /// The emotional connotation of the voice.
        /// </summary>
        public Emotion Emotion { get; set; } = Emotion.Neutral;

        /// <summary>
        /// Create new speech synthesis options.
        /// </summary>
        /// <param name="text">The text to produce speech for.</param>
        /// <param name="speed">The speed (tempo) of the synthesized speech. Must be in range from 0.1 to 3.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public SynthesisOptions(string text, double speed = 1.0)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException(nameof(text));
            if (speed < 0.1 || speed > 3.0)
                throw new ArgumentOutOfRangeException(nameof(speed), speed, "Speed must be in range from 0.1 to 3.");

            Text = text;
            Speed = speed;
        }
    }
}
