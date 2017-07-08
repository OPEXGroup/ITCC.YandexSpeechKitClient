// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ITCC.YandexSpeechKitClient.MessageModels.HttpMode;

namespace ITCC.YandexSpeechKitClient.Utils
{
    internal static class XmlMessageSerializer
    {
        private static readonly XmlSerializer Serializer;
        private static readonly XmlWriterSettings XmlWriterSettings = new XmlWriterSettings { OmitXmlDeclaration = true };
        private static readonly XmlSerializerNamespaces XmlSerializerNamespaces = new XmlSerializerNamespaces(new[]
        {
            new XmlQualifiedName(string.Empty, string.Empty)
        });

        static XmlMessageSerializer() => Serializer = new XmlSerializer(typeof(RecognitionResultsMessage));

        public static RecognitionResultsMessage DeserializeResponse(Stream stream) => (RecognitionResultsMessage)Serializer.Deserialize(stream);
        public static void Serialize(Stream stream, RecognitionResultsMessage recognitionResultsMessage)
        {
            using (var xmlWriter = XmlWriter.Create(stream, XmlWriterSettings))
            {
                Serializer.Serialize(xmlWriter, recognitionResultsMessage, XmlSerializerNamespaces);
            }
        }
    }
}
