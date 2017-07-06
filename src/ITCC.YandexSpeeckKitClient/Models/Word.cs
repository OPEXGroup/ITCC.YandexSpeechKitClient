// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode;

namespace ITCC.YandexSpeeckKitClient.Models
{
    /// <summary>
    /// Word hypothesis message.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Index of word in phrase.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Confidence in the hypothesis for the word.
        /// </summary>
        public float Confidence { get; }

        /// <summary>
        /// Recognition result for the word.
        /// </summary>
        public string Value { get; }

        internal Word(int index, WordMessage wordMessage)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be non-negative.");
            if (wordMessage == null)
                throw new ArgumentNullException(nameof(wordMessage));

            Index = index;
            Confidence = wordMessage.Confidence;
            Value = wordMessage.Value;
        }
    }
}
