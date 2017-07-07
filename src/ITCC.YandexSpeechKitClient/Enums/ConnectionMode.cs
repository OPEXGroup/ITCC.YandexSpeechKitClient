// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace ITCC.YandexSpeeckKitClient.Enums
{
    /// <summary>
    /// Connection security options.
    /// </summary>
    public enum ConnectionMode
    {
        /// <summary>
        /// Encrypted  connection usage.
        /// </summary>
        Secure,

        /// <summary>
        /// Use unencrypted connection.
        /// </summary>
        Insecure
    }
}