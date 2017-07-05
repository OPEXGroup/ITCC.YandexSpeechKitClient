// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Xml.Serialization;

namespace ITCC.YandexSpeeckKitClient.MessageModels.HttpMode
{
    public class VariantMessage
    {
        [XmlAttribute(AttributeName = "confidence")]
        public double Confidence { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}
