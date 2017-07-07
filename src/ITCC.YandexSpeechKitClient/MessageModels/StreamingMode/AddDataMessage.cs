// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    /// <summary>
    /// Speech recognition request message.
    /// </summary>
    [ProtoContract(Name = "AddData")]
    public class AddDataMessage
    {
        /// <summary>
        /// Audio data.
        /// </summary>
        [ProtoMember(1, Name = "audioData", IsRequired = false)]
        public byte[] AudioData;

        /// <summary>
        /// After sending the <see cref="LastChunk"/> = true message to AddData, the server forms the speech recognition results for any audio fragments that have been received but haven‘t been processed yet, and then sends the response. After sending the last response, the server closes the connection.
        /// </summary>
        [ProtoMember(2, Name = "lastChunk", IsRequired = true)]
        public bool LastChunk;
    }
}
