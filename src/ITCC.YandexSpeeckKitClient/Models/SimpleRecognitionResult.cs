// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using ITCC.YandexSpeeckKitClient.MessageModels.HttpMode;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class SimpleRecognitionResult
    {
        public bool Success { get; }
        public List<SimpleVariant> Variants { get; }

        public SimpleRecognitionResult(RecognitionResultsMessage recognitionResultsMessage)
        {
            if (recognitionResultsMessage == null)
                throw new ArgumentNullException(nameof(recognitionResultsMessage));

            if (!recognitionResultsMessage.Success)
                return;

            Success = true;

            if (recognitionResultsMessage.Variants?.Count == 0)
                throw new ArgumentException("Empty variant collection.", nameof(recognitionResultsMessage));

            Variants = recognitionResultsMessage.Variants.Select(message => new SimpleVariant(message)).ToList();
        }
    }
}
