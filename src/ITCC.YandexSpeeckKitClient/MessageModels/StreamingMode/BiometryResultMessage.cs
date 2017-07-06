// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Enums;
using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode
{
    /// <summary>
    /// Biobetric parameter hypothesis message.
    /// </summary>
    [ProtoContract(Name = "BiometryResult")]
    public class BiometryResultMessage
    {
        /// <summary>
        /// Indicator for the biometric.
        /// </summary>
        [ProtoMember(1, Name = "classname", IsRequired = true)]
        public string Classname { get; set; }

        /// <summary>
        /// Result of evaluating the indicator.
        /// </summary>
        [ProtoMember(2, Name = "confidence", IsRequired = true)]
        public float Confidence { get; set; }

        /// <summary>
        /// A biometric that was analyzed, such as <see cref="AgeGroup"/> or <see cref="Gender"/>.
        /// </summary>
        [ProtoMember(3, Name = "tag", IsRequired = false)]
        public string Tag { get; set; }
    }
}
