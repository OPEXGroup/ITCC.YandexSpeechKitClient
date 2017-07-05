// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Enums;
using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    [ProtoContract(Name = "ConnectionResponse")]
    internal class ConnectionResponseMessage
    {
        [ProtoMember(1, Name = "responseCode", IsRequired = true)]
        public ResponseCode ResponseCode { get; set; }

        [ProtoMember(2, Name = "sessionId", IsRequired = true)]
        public string SessionId { get; set; }

        [ProtoMember(3, Name = "message", IsRequired = false)]
        public string Message { get; set; }

        public override string ToString()
        {
            return $"Code = '{ResponseCode} ({(int)ResponseCode})', SessionId = '{SessionId}', Message = '{Message}'.";
        }
    }
}
