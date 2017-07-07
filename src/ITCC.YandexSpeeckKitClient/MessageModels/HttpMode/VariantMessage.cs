// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Xml.Serialization;

namespace ITCC.YandexSpeeckKitClient.MessageModels.HttpMode
{
    /// <summary>
    /// Utterance hypothesis message.
    /// </summary>
    public class VariantMessage
    {
        /// <summary>
        /// Confidence of hypothesis.
        /// </summary>
        [XmlAttribute(AttributeName = "confidence")]
        public float Confidence { get; set; }

        /// <summary>
        /// The normalized recognized text. In a normalized text, numbers are written as digits, and punctuation and abbreviations are included.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}
