// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.Generic;
using System.ComponentModel;
using ITCC.YandexSpeechKitClient.Enums;
using ProtoBuf;

namespace ITCC.YandexSpeechKitClient.MessageModels.StreamingMode
{
    /// <summary>
    /// Speech recognition result message.
    /// </summary>
    [ProtoContract(Name = "AddDataResponse")]
    public class AddDataResponseMessage
    {
        /// <summary>
        /// Code of the server response.
        /// </summary>
        [ProtoMember(1, Name = "responseCode", IsRequired = true)]
        public ResponseCode ResponseCode { get; set; }

        /// <summary>
        /// A set of hypotheses in order of descending confidence.
        /// </summary>
        [ProtoMember(2, Name = "recognition")]
        public List<ResultMessage> Recognition { get; set; }

        /// <summary>
        /// End of the utterance (phrase). If true, the recognition result contains the N-best list of speech recognition hypotheses. If false, the server returns intermediate results in the same structure as the final results, but without details for each word, and with just one hypothesis. In other words, the response contains a single utterance.
        /// </summary>
        [ProtoMember(3, Name = "endOfUtt", IsRequired = false)]
        [DefaultValue(true)]
        public bool EndOfUtterance { get; set; } = false;

        /// <summary>
        /// The number of AddData messages that were combined. A single AddDataResponse is returned for several AddData messages.
        /// </summary>
        [ProtoMember(4, Name = "messagesCount", IsRequired = false)]
        [DefaultValue(true)]
        public int MessagesCount { get; set; } = 1;

        /// <summary>
        /// The result of analyzing the audio signal.
        /// </summary>
        [ProtoMember(6, Name = "bioResult")]
        public List<BiometryResultMessage> BioResult { get; set; }
    }
}
