// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.ComponentModel;
using System.Text;
using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    [ProtoContract(Name = "ConnectionRequest")]
    [ProtoInclude(19, typeof(AdvancedAsrOptionsMessage))]
    internal class ConnectionRequestMessage
    {
        [ProtoMember(1, Name = "protocolVersion", IsRequired = false)]
        [DefaultValue(true)]
        public int ProtocolVersion { get; set; } = 1;

        [ProtoMember(2, Name = "speechkitVersion", IsRequired = true)]
        public string SpeechkitVersion { get; set; }

        [ProtoMember(3, Name = "serviceName", IsRequired = true)]
        public string ServiceName { get; set; }

        [ProtoMember(4, Name = "uuid", IsRequired = true)]
        public string Uuid { get; set; }

        [ProtoMember(5, Name = "apiKey", IsRequired = true)]
        public string ApiKey { get; set; }

        [ProtoMember(6, Name = "applicationName", IsRequired = true)]
        public string ApplicationName { get; set; }

        [ProtoMember(7, Name = "device", IsRequired = true)]
        public string Device { get; set; }

        [ProtoMember(8, Name = "coords", IsRequired = true)]
        public string Coords { get; set; }

        [ProtoMember(9, Name = "topic", IsRequired = true)]
        public string Topic { get; set; }

        [ProtoMember(10, Name = "lang", IsRequired = true)]
        public string Lang { get; set; }

        [ProtoMember(11, Name = "format", IsRequired = true)]
        public string Format { get; set; }

        [ProtoMember(19, Name = "advancedASROptions", IsRequired = false)]
        public AdvancedAsrOptionsMessage AdvancedAsrOptionsMessage { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("CONNECTION REQUEST");
            sb.AppendLine($"{nameof(ProtocolVersion)} : {ProtocolVersion}");
            sb.AppendLine($"{nameof(SpeechkitVersion)} : {SpeechkitVersion}");
            sb.AppendLine($"{nameof(Uuid)} : {Uuid}");
            sb.AppendLine($"{nameof(ApiKey)} : {ApiKey}");
            sb.AppendLine($"{nameof(Device)} : {Device}");
            sb.AppendLine($"{nameof(Coords)} : {Coords}");
            sb.AppendLine($"{nameof(Topic)} : {Topic}");
            sb.AppendLine($"{nameof(Lang)} : {Lang}");
            sb.AppendLine($"{nameof(Format)} : {Format}");

            return sb.ToString();
        }
    }
}
