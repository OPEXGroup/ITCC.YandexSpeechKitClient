// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ITCC.YandexSpeeckKitClient.Utils;

namespace ITCC.YandexSpeeckKitClient.Extensions
{
    internal static class NetworkingExtensions
    {
        private const byte CarriageReturn = 0x0d;
        private const byte LineFeed = 0x0a;

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
        public static Task SendMessageAsync(this Stream stream, object messageObject, CancellationToken cancellationToken)
        {
            return stream.WriteMessageAsync(messageObject, cancellationToken);
        }
        public static async Task<TMessage> GetDeserializedMessageAsync<TMessage>(this Stream stream, CancellationToken cancellationToken)
            where TMessage : class
        {
            var message = await stream.ReadMessageAsync(cancellationToken);
            return BinaryMessageSerializer.Deserialize<TMessage>(message);
        }

        private static async Task WriteMessageAsync(this Stream stream, object messageObject, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var serializedMessage = BinaryMessageSerializer.Serialize(messageObject);
            var sizeBytes = serializedMessage.Length.ToHexBytes();

            await stream.WriteAsync(sizeBytes, 0, sizeBytes.Length, cancellationToken);
            await stream.WriteAsync(ControlSeq, 0, ControlSeq.Length, cancellationToken);
            await stream.WriteAsync(serializedMessage, 0, serializedMessage.Length, cancellationToken);
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
                    var received = await stream.ReadAsync(searchBuffer, 0, searchBufferLength, cancellationToken);
                    if (received == 0)
                        continue;

                    if (searchBuffer[0] != CarriageReturn)
                    {
                        searchMemoryStream.Write(searchBuffer, 0, searchBufferLength);
                        continue;
                    }

                    received = await stream.ReadAsync(searchBuffer, 0, searchBufferLength, cancellationToken);
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
            var receivedBytes = await stream.ReadAsync(messageBytes, 0, messageLength, cancellationToken);

            if (receivedBytes < messageLength)
                throw new EndOfStreamException();

            return messageBytes;
        }
    }
}
