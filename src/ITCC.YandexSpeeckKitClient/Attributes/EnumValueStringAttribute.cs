// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace ITCC.YandexSpeeckKitClient.Attributes
{
    internal class EnumValueStringAttribute : Attribute
    {
        public string Name { get; }

        public EnumValueStringAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }
    }
}
