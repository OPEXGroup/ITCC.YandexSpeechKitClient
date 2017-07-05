// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using ITCC.YandexSpeeckKitClient.Enums;
using ITCC.YandexSpeeckKitClient.MessageModels.StreamingMode;
using JetBrains.Annotations;

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class ChunkRecognitionResult
    {
        public ResponseCode ResponseCode { get; }

        public bool EndOfUtt { get; }
        public int MergedMessagesCount { get; }
        public RecognitionResult Recognition { get; }
        public BiometryResult Biometry { get; }

        internal ChunkRecognitionResult([NotNull] AddDataResponseMessage addDataResponseMessage)
        {
            if (addDataResponseMessage == null)
                throw new ArgumentNullException(nameof(addDataResponseMessage));

            ResponseCode = addDataResponseMessage.ResponseCode;

            if (ResponseCode != ResponseCode.Ok)
                return;

            EndOfUtt = addDataResponseMessage.EndOfUtt;
            MergedMessagesCount = addDataResponseMessage.MessagesCount;

            if (addDataResponseMessage.Recognition != null)
                Recognition = new RecognitionResult(addDataResponseMessage.Recognition);

            if (addDataResponseMessage.BioResult != null)
                Biometry = new BiometryResult(addDataResponseMessage.BioResult);
        }
    }
}
