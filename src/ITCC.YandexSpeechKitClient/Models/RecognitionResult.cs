// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using ITCC.YandexSpeechKitClient.Extensions;
using ITCC.YandexSpeechKitClient.MessageModels.StreamingMode;

namespace ITCC.YandexSpeechKitClient.Models
{
    /// <summary>
    /// Utterance recognition result.
    /// </summary>
    public class RecognitionResult
    {
        /// <summary>
        /// Utterance recognition hypotheses.
        /// </summary>
        public List<UtteranceResult> Utterances { get; }

        /// <summary>
        /// Most reliable utterance hypothesis.
        /// </summary>
        public UtteranceResult MostReliableUtterance => Utterances.MostReliableResult();

        /// <exception cref="ArgumentNullException"></exception>
        internal RecognitionResult(IEnumerable<ResultMessage> resultMessages)
        {
            Utterances = resultMessages?.Select(message => new UtteranceResult(message)).ToList() ?? throw new ArgumentNullException(nameof(resultMessages));
        }
    }
}
