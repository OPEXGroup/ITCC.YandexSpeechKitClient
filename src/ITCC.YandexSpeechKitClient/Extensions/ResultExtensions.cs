// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.Generic;
using System.Linq;
using ITCC.YandexSpeechKitClient.Models;

namespace ITCC.YandexSpeechKitClient.Extensions
{
    internal static class ResultExtensions
    {
        public static TResult MostReliableResult<TResult>(this IEnumerable<TResult> results) where TResult : BaseResultModel
            => results?.OrderByDescending(result => result.Confidence).FirstOrDefault();
    }
}
