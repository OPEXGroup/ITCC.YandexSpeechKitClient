// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.ComponentModel;
using ProtoBuf;

namespace ITCC.YandexSpeechKitClient.MessageModels.StreamingMode
{
    /// <summary>
    /// Start speech recognotion session request message.
    /// </summary>
    [ProtoContract(Name = "ConnectionRequest")]
    [ProtoInclude(19, typeof(AdvancedAsrOptionsMessage))]
    public class ConnectionRequestMessage
    {
        /// <summary>
        /// Version of the protocol.
        /// </summary>
        [ProtoMember(1, Name = "protocolVersion", IsRequired = false)]
        [DefaultValue(true)]
        public int ProtocolVersion { get; set; } = 1;

        /// <summary>
        /// Version of the server-side software. 
        /// </summary>
        [ProtoMember(2, Name = "speechkitVersion", IsRequired = true)]
        public string SpeechkitVersion { get; set; }

        /// <summary>
        /// Name of the service.
        /// </summary>
        [ProtoMember(3, Name = "serviceName", IsRequired = true)]
        public string ServiceName { get; set; }

        /// <summary>
        /// Universally unique identifier.
        /// </summary>
        [ProtoMember(4, Name = "uuid", IsRequired = true)]
        public string Uuid { get; set; }

        /// <summary>
        /// The API key.
        /// </summary>
        [ProtoMember(5, Name = "apiKey", IsRequired = true)]
        public string ApiKey { get; set; }

        /// <summary>
        /// Name of the client application.
        /// </summary>
        [ProtoMember(6, Name = "applicationName", IsRequired = true)]
        public string ApplicationName { get; set; }

        /// <summary>
        /// The type of device running the client application.
        /// </summary>
        [ProtoMember(7, Name = "device", IsRequired = true)]
        public string Device { get; set; }

        /// <summary>
        /// Coordinates of the device running the application.
        /// </summary>
        [ProtoMember(8, Name = "coords", IsRequired = true)]
        public string Coords { get; set; }

        /// <summary>
        /// The language model to use for recognition.
        /// </summary>
        [ProtoMember(9, Name = "topic", IsRequired = true)]
        public string Topic { get; set; }

        /// <summary>
        /// The language for speech recognition.
        /// </summary>
        [ProtoMember(10, Name = "lang", IsRequired = true)]
        public string Lang { get; set; }

        /// <summary>
        /// The audio format, such as audio/x-speex.
        /// </summary>
        [ProtoMember(11, Name = "format", IsRequired = true)]
        public string Format { get; set; }

        /// <summary>
        /// The response will contain biometric characteristics of the recognized speech and the intermediate speech recognition results.
        /// </summary>
        [ProtoMember(19, Name = "advancedASROptions", IsRequired = false)]
        public AdvancedAsrOptionsMessage AdvancedAsrOptionsMessage { get; set; }
    }
}
