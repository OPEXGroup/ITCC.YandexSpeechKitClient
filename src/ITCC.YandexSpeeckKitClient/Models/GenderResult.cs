﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Enums;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class GenderResult : BaseResultModel
    {
        public Gender Gender { get; }

        public GenderResult(float confidence, Gender gender) : base(confidence)
        {
            Gender = gender;
        }
    }
}
