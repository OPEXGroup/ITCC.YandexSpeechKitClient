// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using ITCC.YandexSpeechKitClient.MessageModels.HttpMode;

namespace ITCC.YandexSpeechKitClient.Models
{
    /// <summary>
    /// Simple utterance recognition result.
    /// </summary>
    public class UtteranceRecognitionResult
    {
        /// <summary>
        /// Indicates if speech was recognized.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// List of utterance hypothesis. Property is not null if recognition succeed.
        /// </summary>
        public List<UtteranceVariant> Variants { get; }

        internal UtteranceRecognitionResult(RecognitionResultsMessage recognitionResultsMessage)
        {
            if (recognitionResultsMessage == null)
                throw new ArgumentNullException(nameof(recognitionResultsMessage));

            if (!recognitionResultsMessage.Success)
                return;

            Success = true;

            if (recognitionResultsMessage.Variants?.Any() != true)
                throw new ArgumentException("Empty variant collection.", nameof(recognitionResultsMessage));

            Variants = recognitionResultsMessage.Variants.Select(message => new UtteranceVariant(message)).ToList();
        }
    }
}
