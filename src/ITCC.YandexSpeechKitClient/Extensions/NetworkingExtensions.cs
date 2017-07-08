// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ITCC.YandexSpeechKitClient.Utils;

namespace ITCC.YandexSpeechKitClient.Extensions
{
    internal static class NetworkingExtensions
    {
        private const byte CarriageReturn = 0x0d;
        private const byte LineFeed = 0x0a;
        private const int BufferSize = 81920;

        private static readonly byte[] ControlSeq = { CarriageReturn, LineFeed };

        public static async Task<byte[]> ReceiveAllBytesAsync(this Stream stream, CancellationToken cancellationToken)
        {
            using (var ms = new MemoryStream())
            {
                var buffer = new byte[2048];

                while (true)
                {
                    var readCount = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);

                    ms.Write(buffer, 0, readCount);

                    if (readCount < buffer.Length)
                        break;
                }
                ms.Position = 0;
                return ms.ToArray();
            }
        }
        public static async Task SendMessageAsync(this Stream stream, object messageObject, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var serializedMessage = BinaryMessageSerializer.Serialize(messageObject);
            var sizeBytes = serializedMessage.Length.ToHexBytes();

            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(sizeBytes, 0, sizeBytes.Length);
                memoryStream.Write(ControlSeq, 0, ControlSeq.Length);
                memoryStream.Write(serializedMessage, 0, serializedMessage.Length);

                memoryStream.Seek(0, SeekOrigin.Begin);

                await memoryStream.CopyToAsync(stream, BufferSize, cancellationToken).ConfigureAwait(false);
            }
        }
        public static async Task<TMessage> GetDeserializedMessageAsync<TMessage>(this Stream stream, CancellationToken cancellationToken)
            where TMessage : class
        {
            var message = await stream.ReadMessageAsync(cancellationToken).ConfigureAwait(false);
            return BinaryMessageSerializer.Deserialize<TMessage>(message);
        }

        private static async Task<byte[]> ReadMessageAsync(this Stream stream, CancellationToken cancellationToken)
        {
            byte[] sizeHexBytes;
            using (var searchMemoryStream = new MemoryStream())
            {
                var searchBuffer = new byte[1];
                var searchBufferLength = searchBuffer.Length;
                while (true)
                {
                    var received = await stream.ReadAsync(searchBuffer, 0, searchBufferLength, cancellationToken).ConfigureAwait(false);
                    if (received == 0)
                        continue;

                    if (searchBuffer[0] != CarriageReturn)
                    {
                        searchMemoryStream.Write(searchBuffer, 0, searchBufferLength);
                        continue;
                    }

                    received = await stream.ReadAsync(searchBuffer, 0, searchBufferLength, cancellationToken).ConfigureAwait(false);
                    if (received == 0)
                        throw new EndOfStreamException();

                    if (searchBuffer[0] != LineFeed)
                    {
                        searchMemoryStream.Write(searchBuffer, 0, searchBufferLength);
                        continue;
                    }

                    break;
                }
                sizeHexBytes = searchMemoryStream.ToArray();
            }

            var messageLength = sizeHexBytes.FromHexBytes();
            var messageBytes = new byte[messageLength];
            var receivedBytes = await stream.ReadAsync(messageBytes, 0, messageLength, cancellationToken).ConfigureAwait(false);

            if (receivedBytes < messageLength)
                throw new EndOfStreamException();

            return messageBytes;
        }
    }
}
