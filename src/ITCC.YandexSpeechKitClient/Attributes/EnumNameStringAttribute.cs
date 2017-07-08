// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace ITCC.YandexSpeechKitClient.Attributes
{
    [AttributeUsage(AttributeTargets.Enum)]
    internal class EnumNameStringAttribute : Attribute
    {
        public string Name { get; }
        public EnumNameStringAttribute(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            Name = name;
        }
    }
}
