// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.Generic;
using ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode;
using JetBrains.Annotations;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class PhraseResult : BaseResultModel
    {
        public bool IsIntermediate { get; }
        public string NormalizedPhrase { get; }
        public List<Word> Words { get; }

        internal PhraseResult([NotNull] ResultMessage resultMessage) : base(resultMessage.Confidence)
        {
            NormalizedPhrase = resultMessage.Normalized;

            if (resultMessage.Words == null)
            {
                IsIntermediate = true;
            }
            else
            {
                Words = new List<Word>();
                var wordIndex = 0;
                foreach (var wordMessage in resultMessage.Words)
                {
                    Words.Add(new Word(wordIndex, wordMessage));
                    wordIndex++;
                }
            }
        }
    }
}
