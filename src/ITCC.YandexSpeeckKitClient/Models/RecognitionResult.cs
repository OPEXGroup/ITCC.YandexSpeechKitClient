// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.Generic;
using System.Linq;
using ITCC.YandexSpeeckKitClient.Extensions;
using ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode;
using JetBrains.Annotations;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class RecognitionResult
    {
        public List<PhraseResult> Phrases { get; }
        public PhraseResult MostReliablePhrase => Phrases.MostReliableResult();

        internal RecognitionResult([NotNull, ItemNotNull] IEnumerable<ResultMessage> resultMessages)
        {
            Phrases = resultMessages.Select(message => new PhraseResult(message)).ToList();
        }
    }
}
