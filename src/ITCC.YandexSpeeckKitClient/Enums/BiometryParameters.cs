// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    [Flags]
    public enum BiometryParameters
    {
        None = 1 << 0,
        [EnumValueString("gender")]
        Gender = 1 << 1,
        [EnumValueString("group")]
        Group = 1 << 2,
        [EnumValueString("language")]
        Language = 1 << 3
    }
}