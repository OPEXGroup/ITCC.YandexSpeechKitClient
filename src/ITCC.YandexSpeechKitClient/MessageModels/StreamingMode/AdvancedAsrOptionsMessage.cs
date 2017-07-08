// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.ComponentModel;
using ProtoBuf;

namespace ITCC.YandexSpeechKitClient.MessageModels.StreamingMode
{
    /// <summary>
    /// Advanced speech recognition options.
    /// </summary>
    [ProtoContract(Name = "AdvancedASROptions")]
    public class AdvancedAsrOptionsMessage
    {
        /// <summary>
        /// Incomplete speech recognition results are returned in the response if the true value is passed. If false is passed, the response only contains the final speech recognition results for the phrase or word.
        /// </summary>
        [ProtoMember(1, Name = "partial_results", IsRequired = false)]
        [DefaultValue(true)]
        public bool PartialResults { get; set; } = true;

        /// <summary>
        /// Characteristics to identify during voice analysis.
        /// </summary>
        [ProtoMember(24, Name = "biometry", IsRequired = false)]
        public string Biometry { get; set; }
    }
}
