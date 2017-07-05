// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Enums;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class AgeGroupResult : BaseResultModel
    {
        public AgeGroup Group { get; }

        public AgeGroupResult(float confidence, AgeGroup ageGroup) : base(confidence)
        {
            Group = ageGroup;
        }
    }
}
