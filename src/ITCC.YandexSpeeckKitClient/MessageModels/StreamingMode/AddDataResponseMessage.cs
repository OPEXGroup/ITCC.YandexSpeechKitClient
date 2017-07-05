// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ITCC.YandexSpeeckKitClient.Enums;
using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    [ProtoContract(Name = "AddDataResponse")]
    internal class AddDataResponseMessage
    {
        [ProtoMember(1, Name = "responseCode", IsRequired = true)]
        public ResponseCode ResponseCode { get; set; }

        [ProtoMember(2, Name = "recognition")]
        public List<ResultMessage> Recognition { get; set; }

        [ProtoMember(3, Name = "endOfUtt", IsRequired = false)]
        [DefaultValue(true)]
        public bool EndOfUtt { get; set; } = false;

        [ProtoMember(4, Name = "messagesCount", IsRequired = false)]
        [DefaultValue(true)]
        public int MessagesCount { get; set; } = 1;

        [ProtoMember(6, Name = "bioResult")]
        public List<BiometryResultMessage> BioResult { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder($"AddDataResponse:{Environment.NewLine}");

            stringBuilder.AppendLine($"Response code:\t[{(int)ResponseCode}] {ResponseCode}");
            stringBuilder.AppendLine($"EndOfUtt:\t{EndOfUtt}");
            stringBuilder.AppendLine($"MessagesCount:\t{MessagesCount}");
            if (BioResult?.Count > 0)
            {
                stringBuilder.AppendLine("BioResult:");
                foreach (var biometryResult in BioResult)
                {
                    stringBuilder.AppendLine(
                        $"\tClass = '{biometryResult.Classname}, tag = '{biometryResult.Tag}' confidence = '{biometryResult.Confidence};'");
                }
            }
            if (Recognition?.Count > 0)
            {
                stringBuilder.AppendLine("Recognition:");

                foreach (var result in Recognition)
                {
                    stringBuilder.AppendLine($"\nConfidence = '{result.Confidence}', normalized = '{result.Normalized}'");

                    if (result.Words?.Count > 0)
                    {
                        var words = string.Join(", ", result.Words.Select(word => $"[{word.Confidence}]'{word.Value}'"));
                        stringBuilder.AppendLine($"Words: {words}");
                    }

                }
            }

            stringBuilder.AppendLine($"{Environment.NewLine}- END OF RESPONSE -{Environment.NewLine}");

            return stringBuilder.ToString();
        }
    }
}
