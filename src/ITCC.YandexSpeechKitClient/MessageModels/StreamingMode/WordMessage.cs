// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ProtoBuf;

namespace ITCC.YandexSpeechKitClient.MessageModels.StreamingMode
{
    /// <summary>
    /// Word hypothesis message.
    /// </summary>
    [ProtoContract(Name = "Word")]
    public class WordMessage
    {
        /// <summary>
        /// Confidence in the hypothesis for the word.
        /// </summary>
        [ProtoMember(1, Name = "confidence", IsRequired = true)]
        public float Confidence { get; set; }

        /// <summary>
        /// Recognition result for the word.
        /// </summary>
        [ProtoMember(2, Name = "value", IsRequired = true)]
        public string Value { get; set; }
    }
}
