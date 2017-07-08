// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.Generic;
using System.Xml.Serialization;

namespace ITCC.YandexSpeechKitClient.MessageModels.HttpMode
{
    /// <summary>
    /// Recognition result message.
    /// </summary>
    [XmlRoot(ElementName = "recognitionResults")]
    public class RecognitionResultsMessage
    {
        /// <summary>
        /// Indicates if recognition succeeded.
        /// </summary>
        [XmlAttribute(AttributeName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// List of utterance hypothesis. Property is not null if recognition succeed.
        /// </summary>
        [XmlElement(ElementName = "variant")]
        public List<VariantMessage> Variants { get; set; }
    }
}
