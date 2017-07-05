// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    [ProtoContract(Name = "AddData")]
    internal class AddDataMessage
    {
        [ProtoMember(1, Name = "audioData", IsRequired = false)]
        public byte[] AudioData;
        [ProtoMember(2, Name = "lastChunk", IsRequired = true)]
        public bool LastChunk;
    }
}
