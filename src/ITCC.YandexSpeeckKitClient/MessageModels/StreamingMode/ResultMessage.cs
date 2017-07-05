// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.Generic;
using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    [ProtoContract(Name = "Result")]
    internal class ResultMessage
    {
        [ProtoMember(1, Name = "confidence", IsRequired = true)]
        public float Confidence { get; set; }

        [ProtoMember(2, Name = "words")]
        public List<WordMessage> Words { get; set; }

        [ProtoMember(3, Name = "normalized", IsRequired = false)]
        public string Normalized { get; set; }
    }
}
