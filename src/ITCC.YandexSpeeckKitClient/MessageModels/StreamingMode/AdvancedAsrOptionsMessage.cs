// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.ComponentModel;
using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    [ProtoContract(Name = "AdvancedASROptions")]
    internal class AdvancedAsrOptionsMessage
    {
        [ProtoMember(1, Name = "partial_results", IsRequired = false)]
        [DefaultValue(true)]
        public bool PartialResults { get; set; } = true;

        [ProtoMember(24, Name = "biometry", IsRequired = false)]
        public string Biometry { get; set; }
    }
}
