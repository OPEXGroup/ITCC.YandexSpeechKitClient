// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.Generic;
using System.Xml.Serialization;

namespace ITCC.YandexSpeeckKitClient.MessageModels.HttpMode
{
    [XmlRoot(ElementName = "recognitionResults")]
    public class RecognitionResultsMessage
    {
        [XmlAttribute(AttributeName = "success")]
        public bool Success { get; set; }
        [XmlElement(ElementName = "variant")]
        public List<VariantMessage> Variants { get; set; }
    }
}
