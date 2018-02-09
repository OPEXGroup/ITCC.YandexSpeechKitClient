// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace ITCC.YandexSpeechKitClient
{
    /// <summary>
    /// SpeechKit Cloud API access options.
    /// </summary>
    public class SpeechKitClientOptions
    {
        /// <summary>
        /// The API key.
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Name of the client application.
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// User's identifier.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// The type of device running the client application.
        /// </summary>
        public string Device { get; }

        /// <summary>
        /// Data streaming operations timeout in milliseconds.
        /// </summary>
        public int Timeout { get; }

        /// <summary>
        /// Create new API client options.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="applicationName">Name of the client application.</param>
        /// <param name="userId">User's identifier.</param>
        /// <param name="device">The type of device running the client application.</param>
        /// <param name="timeout">Data streaming operation's timeout in milliseconds.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public SpeechKitClientOptions(string apiKey, string applicationName, Guid userId, string device, int timeout = Configuration.DefaultTimeout)
        {
            ApiKey = apiKey == null 
                ? throw new ArgumentNullException(nameof(apiKey))
                : string.IsNullOrWhiteSpace(apiKey)
                    ? throw new ArgumentException(nameof(apiKey))
                    : apiKey;

            ApplicationName = applicationName == null
                ? throw new ArgumentNullException(nameof(applicationName))
                : string.IsNullOrWhiteSpace(apiKey)
                    ? throw new ArgumentException(nameof(applicationName))
                    : applicationName;

            Device = device == null
                ? throw new ArgumentNullException(nameof(device))
                : string.IsNullOrWhiteSpace(apiKey)
                    ? throw new ArgumentException(nameof(device))
                    : device;

            UserId = userId;
            Timeout = timeout;
        }

        /// <summary>
        /// Create new API client options.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="applicationName">Name of the client application.</param>
        /// <param name="userId">User's identifier.</param>
        /// <param name="device">The type of device running the client application.</param>
        /// <param name="timeout">Data streaming operation's timeout.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public SpeechKitClientOptions(string apiKey, string applicationName, Guid userId, string device,
            TimeSpan timeout) : this(apiKey, applicationName, userId, device, (int) timeout.TotalMilliseconds)
        {
        }
    }
}
