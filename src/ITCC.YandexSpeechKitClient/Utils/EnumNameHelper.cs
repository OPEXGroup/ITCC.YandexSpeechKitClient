// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Utils
{
    internal static class EnumNameHelper
    {
        private static readonly Dictionary<Type, string> EnumNameDictionary = new Dictionary<Type, string>();

        static EnumNameHelper()
        {
            var assembly = typeof(EnumNameHelper).GetTypeInfo().Assembly;
            foreach (var enumTypeInfo in assembly.DefinedTypes.Where(typeInfo => typeInfo.IsEnum))
            {
                if (!enumTypeInfo.IsDefined(typeof(EnumNameStringAttribute)))
                    continue;

                var enumName = enumTypeInfo.GetCustomAttribute<EnumNameStringAttribute>().Name;
                EnumNameDictionary.Add(enumTypeInfo.AsType(), enumName);
            }
        }

        public static string GetEnumStringName<TEnum>() where TEnum : struct, IConvertible
            => GetEnumStringName(typeof(TEnum));
        public static string GetEnumStringName(Type enumType)
        {
            return EnumNameDictionary.TryGetValue(enumType, out string nameValue) ? nameValue : null;
        }
    }
}
