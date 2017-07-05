// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public abstract class BaseResultModel
    {
        public float Confidence { get; }

        protected BaseResultModel(float confidence)
        {
            if (confidence < 0 || confidence > 1)
                throw new ArgumentOutOfRangeException(nameof(confidence), "Confidence must be between 0 and 1.");

            Confidence = confidence;
        }
    }
}
