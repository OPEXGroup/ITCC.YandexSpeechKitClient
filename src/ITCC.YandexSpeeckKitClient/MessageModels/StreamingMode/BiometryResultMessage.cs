// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    [ProtoContract(Name = "BiometryResult")]
    internal class BiometryResultMessage
    {
        [ProtoMember(1, Name = "classname", IsRequired = true)]
        public string Classname { get; set; }

        [ProtoMember(2, Name = "confidence", IsRequired = true)]
        public float Confidence { get; set; }

        [ProtoMember(3, Name = "tag", IsRequired = false)]
        public string Tag { get; set; }
    }
}
