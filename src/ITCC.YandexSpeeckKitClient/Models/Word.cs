// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode;
using JetBrains.Annotations;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class Word
    {
        public int Index { get; }
        public float Confidence { get; }
        public string Value { get; }

        internal Word(int index, [NotNull] WordMessage wordMessage)
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
