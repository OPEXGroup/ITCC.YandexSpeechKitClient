// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.Generic;
using ITCC.YandexSpeechKitClient.MessageModels.StreamingMode;

namespace ITCC.YandexSpeechKitClient.Models
{
    /// <summary>
    /// Utterance hypothesis.
    /// </summary>
    public class UtteranceResult : BaseResultModel
    {
        /// <summary>
        /// Indicates if the result is intermediate. The intermediate result contains just one hypothesis for the entire utterance.
        /// </summary>
        public bool IsIntermediate { get; }

        /// <summary>
        /// The normalized recognized text. In a normalized text, numbers are written as digits, and punctuation and abbreviations are included.
        /// </summary>
        public string NormalizedText { get; }

        /// <summary>
        /// Words in the utterance. Proterty is null for intermediate results.
        /// </summary>
        public List<Word> Words { get; }

        internal UtteranceResult(ResultMessage resultMessage) : base(resultMessage.Confidence)
        {
            NormalizedText = resultMessage.Normalized;

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
