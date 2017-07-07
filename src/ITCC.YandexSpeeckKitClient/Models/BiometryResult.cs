// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using ITCC.YandexSpeeckKitClient.Enums;
using ITCC.YandexSpeeckKitClient.Extensions;
using ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode;
using ITCC.YandexSpeeckKitClient.Utils;

namespace ITCC.YandexSpeeckKitClient.Models
{
    /// <summary>
    /// Speech analysis result. 
    /// </summary>
    public class BiometryResult
    {
        /// <summary>
        /// Gender analysis hypotheses.
        /// </summary>
        public List<GenderResult> GenderResults { get; }

        /// <summary>
        /// Age group analysis hypotheses.
        /// </summary>
        public List<AgeGroupResult> AgeGroupResults { get; }

        /// <summary>
        /// Language analysis hypotheses.
        /// </summary>
        public List<LanguageResult> LanguageResults { get; }

        /// <summary>
        /// Most reliable speaker gender hypothesis.
        /// </summary>
        public GenderResult Gender => GenderResults?.MostReliableResult();

        /// <summary>
        /// Most reliable speaker age group hypothesis.
        /// </summary>
        public AgeGroupResult AgeGroup => AgeGroupResults?.MostReliableResult();

        /// <summary>
        /// Most reliable speaker language hypothesis.
        /// </summary>
        public LanguageResult Language => LanguageResults?.MostReliableResult();

        internal BiometryResult(IEnumerable<BiometryResultMessage> biometryResultMessages)
        {
            if (biometryResultMessages == null)
                throw new ArgumentNullException(nameof(biometryResultMessages));

            foreach (var grouping in biometryResultMessages.GroupBy(message => message.Tag))
            {
                if (grouping.Key == EnumNameHelper.GetEnumStringName<Gender>())
                {
                    GenderResults = grouping
                        .Select(biometryResultMessage =>
                            new GenderResult(biometryResultMessage.Confidence, biometryResultMessage.Classname.GetEnumElementByName<Gender>()))
                        .ToList();
                }
                else if (grouping.Key == EnumNameHelper.GetEnumStringName<AgeGroup>())
                {
                    AgeGroupResults = grouping
                        .Select(biometryResultMessage =>
                            new AgeGroupResult(biometryResultMessage.Confidence, biometryResultMessage.Classname.GetEnumElementByName<AgeGroup>()))
                        .ToList();
                }
                else if (grouping.Key == EnumNameHelper.GetEnumStringName<RecognitionLanguage>())
                {
                    LanguageResults = grouping
                        .Select(biometryResultMessage =>
                            new LanguageResult(biometryResultMessage.Confidence, biometryResultMessage.Classname.GetEnumElementByName<DetectedLanguage>()))
                        .ToList();
                }
                else
                {
                    throw new ArgumentException($"Unknown tag: '{grouping.Key}'.", nameof(biometryResultMessages));
                }
            }
        }
    }
}
