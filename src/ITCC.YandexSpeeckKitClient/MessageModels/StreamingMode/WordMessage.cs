// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    [ProtoContract(Name = "Word")]
    internal class WordMessage
    {
        [ProtoMember(1, Name = "confidence", IsRequired = true)]
        public float Confidence { get; set; }

        [ProtoMember(2, Name = "value", IsRequired = true)]
        public string Value { get; set; }
    }
}
