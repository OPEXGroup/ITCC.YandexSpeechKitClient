// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Globalization;
using System.Text;

namespace ITCC.YandexSpeechKitClient.Extensions
{
    internal static class ConvertExtensions
    {
        public static string ToHex(this int value) => $"{value:X}";
        public static int FromHex(this string hexString) => int.Parse(hexString, NumberStyles.HexNumber);

        public static byte[] ToHexBytes(this int value) => Encoding.UTF8.GetBytes(value.ToHex());
        public static int FromHexBytes(this byte[] bytes) => Encoding.UTF8.GetString(bytes).FromHex();

        public static string ToUuid(this Guid guid) => guid.ToString().Replace("-", "");
    }
}
