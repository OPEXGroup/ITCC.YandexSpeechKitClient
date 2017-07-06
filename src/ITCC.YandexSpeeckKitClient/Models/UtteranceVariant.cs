// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeeckKitClient.MessageModels.HttpMode;

namespace ITCC.YandexSpeeckKitClient.Models
{
    /// <summary>
    /// Utterance hypothesis.
    /// </summary>
    public class UtteranceVariant
    {
        /// <summary>
        /// Confidence of hypothesis.
        /// </summary>
        public double Confidence { get; }

        /// <summary>
        /// The normalized recognized text. In a normalized text, numbers are written as digits, and punctuation and abbreviations are included.
        /// </summary>
        public string Text { get; }

        internal UtteranceVariant(VariantMessage variantMessage)
        {
            if (variantMessage == null)
                throw new ArgumentNullException(nameof(variantMessage));

            Confidence = variantMessage.Confidence;
            Text = variantMessage.Value;
        }
    }
}
