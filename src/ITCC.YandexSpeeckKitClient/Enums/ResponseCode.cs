// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ProtoBuf;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    [ProtoContract]
    public enum ResponseCode
    {
        [ProtoEnum(Name = "ОК", Value = 200)]
        Ok = 200,

        [ProtoEnum(Name = "BadMessageFormatting", Value = 400)]
        BadMessageFormatting = 400,

        [ProtoEnum(Name = "UnknownService", Value = 404)]
        UnknownService = 404,

        [ProtoEnum(Name = "NotSupportedVersion", Value = 405)]
        NotSupportedVersion = 405,

        [ProtoEnum(Name = "Timeout", Value = 408)]
        Timeout = 408,

        [ProtoEnum(Name = "ProtocolError", Value = 410)]
        ProtocolError = 410,

        [ProtoEnum(Name = "InternalError", Value = 500)]
        InternalError = 500,
    }
}