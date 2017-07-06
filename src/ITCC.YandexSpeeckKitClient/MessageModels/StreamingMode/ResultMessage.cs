// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.Generic;
using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    /// <summary>
    /// Utterance hypothesis message.
    /// </summary>
    [ProtoContract(Name = "Result")]
    public class ResultMessage
    {
        /// <summary>
        /// Confidence in the hypothesis for the entire utterance.
        /// </summary>
        [ProtoMember(1, Name = "confidence", IsRequired = true)]
        public float Confidence { get; set; }

        /// <summary>
        /// Words in the utterance.
        /// </summary>
        [ProtoMember(2, Name = "words")]
        public List<WordMessage> Words { get; set; }

        /// <summary>
        /// The normalized recognized text. In a normalized text, numbers are written as digits, and punctuation and abbreviations are included.
        /// </summary>
        [ProtoMember(3, Name = "normalized", IsRequired = false)]
        public string Normalized { get; set; }
    }
}
