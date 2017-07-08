// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Diagnostics;
using System.IO;
using ITCC.YandexSpeechKitClient.MessageModels.StreamingMode;
using ProtoBuf.Meta;

namespace ITCC.YandexSpeechKitClient.Utils
{
    internal static class BinaryMessageSerializer
    {
        private static readonly TypeModel Model;

        static BinaryMessageSerializer()
        {
            var model = TypeModel.Create();

            model.Add(typeof(AdvancedAsrOptionsMessage), true);
            model.Add(typeof(ConnectionRequestMessage), true);
            model.Add(typeof(AddDataMessage), true);
            model.Add(typeof(ConnectionResponseMessage), true);
            model.Add(typeof(WordMessage), true);
            model.Add(typeof(ResultMessage), true);
            model.Add(typeof(BiometryResultMessage), true);
            model.Add(typeof(AddDataResponseMessage), true);

            Model = model.Compile();
        }


        public static void Serialize(Stream stream, object obj) => Model.Serialize(stream, obj);

        public static byte[] Serialize(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                Model.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static TObject Deserialize<TObject>(byte[] bytes) where TObject : class
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                try
                {
                    memoryStream.Position = 0;
                    return (TObject)Model.Deserialize(memoryStream, null, typeof(TObject));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.StackTrace);
                    throw;
                }
            }
        }
    }
}
