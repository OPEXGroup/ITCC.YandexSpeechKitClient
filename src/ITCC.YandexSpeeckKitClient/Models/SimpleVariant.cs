// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeeckKitClient.MessageModels.HttpMode;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class SimpleVariant
    {
        public double Confidence { get; }
        public string Text { get; }

        public SimpleVariant(VariantMessage variantMessage)
        {
            if (variantMessage == null)
                throw new ArgumentNullException(nameof(variantMessage));

            Confidence = variantMessage.Confidence;
            Text = variantMessage.Value;
        }
    }
}
