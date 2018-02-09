// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeechKitClient.Enums;
using ProtoBuf;

namespace ITCC.YandexSpeechKitClient.MessageModels.StreamingMode
{
    /// <summary>
    /// Start speech recognition session response message.
    /// </summary>
    [ProtoContract(Name = "ConnectionResponse")]
    public class ConnectionResponseMessage
    {
        /// <summary>
        /// The response code.
        /// </summary>
        [ProtoMember(1, Name = "responseCode", IsRequired = true)]
        public ResponseCode ResponseCode { get; set; }

        /// <summary>
        /// The session ID. Specify this ID when contacting tech support.
        /// </summary>
        [ProtoMember(2, Name = "sessionId", IsRequired = true)]
        public string SessionId { get; set; }

        /// <summary>
        /// Error message text. Included in the response if the response code is something other than 200.
        /// </summary>
        [ProtoMember(3, Name = "message", IsRequired = false)]
        public string Message { get; set; }
    }
}
