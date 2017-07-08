// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ProtoBuf;

namespace ITCC.YandexSpeechKitClient.Enums
{
    /// <summary>
    /// Start speech recognition session respose code.
    /// </summary>
    [ProtoContract]
    public enum ResponseCode
    {
        /// <summary>
        /// ОК (success).
        /// </summary>
        [ProtoEnum(Name = "ОК", Value = 200)]
        Ok = 200,

        /// <summary>
        /// Missing topic or language.
        /// </summary>
        [ProtoEnum(Name = "BadMessageFormatting", Value = 400)]
        BadMessageFormatting = 400,

        /// <summary>
        /// Missing or invalid service name.
        /// </summary>
        [ProtoEnum(Name = "UnknownService", Value = 404)]
        UnknownService = 404,

        /// <summary>
        /// Unsupported version specified.
        /// </summary>
        [ProtoEnum(Name = "NotSupportedVersion", Value = 405)]
        NotSupportedVersion = 405,

        /// <summary>
        /// Automatic logoff due to inactivity in the client application.
        /// </summary>
        [ProtoEnum(Name = "Timeout", Value = 408)]
        Timeout = 408,

        /// <summary>
        /// Audio data not transmitted (when lastChunk=true).
        /// </summary>
        [ProtoEnum(Name = "ProtocolError", Value = 410)]
        ProtocolError = 410,

        /// <summary>
        /// Invalid API key.
        /// </summary>
        [ProtoEnum(Value = 429)]
        InvalidApiKey = 429,

        /// <summary>
        /// Speech recognition failed on the server due to an internal error.
        /// </summary>
        [ProtoEnum(Name = "InternalError", Value = 500)]
        InternalError = 500,
    }
}