// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Utils
{
    internal static class EnumValueNameHelper
    {
        #region public

        public static string GetEnumString(this object element)
        {
            var attributes = GetAttributes(element);
            return attributes == null ? null : string.Join(Separator, attributes.Select(attr => attr.Name));
        }
        public static object GetEnumElementByName(this string name, Type enumType)
        {
            if (name == null)
                return null;

            var dictionary = GetInfoDictionaty(enumType);
            if (dictionary == null)
                return null;

            if (IsFlagEnum(enumType))
            {
                var list = new List<object>();
                var stringValues = name.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var stringValue in stringValues)
                {
                    if (dictionary.Values.Any(value => value?.Name == stringValue))
                        list.Add(dictionary.First(keyValuePair => keyValuePair.Value.Name == stringValue).Key);
                    else
                        return null;
                }

                return list.Aggregate(0, (current, elem) => current | (int)elem);
            }

            return dictionary.Values.All(value => value?.Name != name) ? null : dictionary.FirstOrDefault(keyValuePair => keyValuePair.Value.Name == name).Key;
        }
        public static TEnum GetEnumElementByName<TEnum>(this string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var dictionary = GetInfoDictionaty<TEnum>();
            if (dictionary == null)
                throw new ArgumentException($"Unknown enum type: '{typeof(TEnum)}'.", nameof(name));

            if (IsFlagEnum<TEnum>())
            {
                var list = new List<TEnum>();
                var stringValues = name.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var stringValue in stringValues)
                {
                    if (dictionary.Values.Any(value => value?.Name == stringValue))
                        list.Add(dictionary.First(keyValuePair => keyValuePair.Value.Name == stringValue).Key);
                    else
                        return default(TEnum);
                }

                return (TEnum)(object)list.Aggregate(0, (current, elem) => current | (int)(object)elem);
            }

            return dictionary.Values.All(value => value?.Name != name) ? default(TEnum) : dictionary.First(keyValuePair => keyValuePair.Value.Name == name).Key;
        }

        #endregion

        #region private

        private const string Separator = ",";
        private static readonly ConcurrentDictionary<Type, Dictionary<object, EnumValueStringAttribute>> Dictionary = new ConcurrentDictionary<Type, Dictionary<object, EnumValueStringAttribute>>();
        private static IList<EnumValueStringAttribute> GetAttributes(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            var objType = value.GetType();
            if (!objType.GetTypeInfo().IsEnum)
                return null;

            var enumValue = (Enum)value;
            var dictionary = GetInfoDictionaty(objType);
            if (IsFlagEnum(objType))
            {
                var splittedEnum = ((IEnumerable)typeof(EnumValueNameHelper).GetTypeInfo()
                    .GetDeclaredMethod(nameof(SplitEnum))
                    .MakeGenericMethod(objType)
                    .Invoke(null, new object[] { enumValue })
                ).Cast<Enum>();

                return splittedEnum.Select(elem => !dictionary.TryGetValue(elem, out EnumValueStringAttribute attribute) ? null : attribute).ToList();
            }
            dictionary.TryGetValue(value, out EnumValueStringAttribute enumValueInfoAttribute);

            return enumValueInfoAttribute == null ? null : new List<EnumValueStringAttribute> { enumValueInfoAttribute };
        }
        private static bool IsFlagEnum(Type enumType) => enumType.GetTypeInfo().GetCustomAttributes<FlagsAttribute>().Any();
        private static bool IsFlagEnum<TEnum>() => typeof(TEnum).GetTypeInfo().GetCustomAttributes<FlagsAttribute>().Any();
        private static Dictionary<object, EnumValueStringAttribute> GetInfoDictionaty(Type enumType)
        {
            if (!enumType.GetTypeInfo().IsEnum)
                return null;

            Dictionary<object, EnumValueStringAttribute> dictionary;
            if (!Dictionary.ContainsKey(enumType))
            {
                dictionary = Enum.GetValues(enumType)
                    .Cast<object>()
                    .ToDictionary(
                        elem => elem,
                        elem => elem.GetType()
                            .GetRuntimeField(elem.ToString())
                            .GetCustomAttributes<EnumValueStringAttribute>()
                            .FirstOrDefault());
                Dictionary.TryAdd(enumType, dictionary);
            }
            else
            {
                dictionary = Dictionary[enumType];
            }
            return dictionary;
        }
        private static Dictionary<TEnum, EnumValueStringAttribute> GetInfoDictionaty<TEnum>()
            => GetInfoDictionaty(typeof(TEnum))?.ToDictionary(keyValuePair => (TEnum)keyValuePair.Key, keyValuePair => keyValuePair.Value);
        private static IEnumerable<TEnum> SplitEnum<TEnum>(TEnum value)
            => Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Where(elem =>
                {
                    var enumValue = value as Enum;
                    var enumElement = elem as Enum;
                    // ReSharper disable once PossibleInvalidCastException
                    return enumElement != null && enumValue != null && (int)(object)enumElement != 0 && enumValue.HasFlag(enumElement);
                });

        #endregion
    }
}
