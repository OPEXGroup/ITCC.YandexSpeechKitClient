// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using ITCC.YandexSpeeckKitClient.Models;

namespace ITCC.YandexSpeeckKitClient
{
    /// <summary>
    /// Factory for speech recognition sessions creation (data streaming mode).
    /// </summary>
    public static class SpeechRecognitionSessionFactory
    {
        /// <summary>
        /// Start new speech recognition session in data streaming mode.
        /// </summary>
        /// <param name="apiOptions">API access options.</param>
        /// <param name="sessionOptions">Recognition settings.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns></returns>
        public static Task<StartSessionResult> CreateNewSpeechRecognitionSessionAsync(
            SpeechKitClientOptions apiOptions, 
            SpeechRecognitionSessionOptions sessionOptions, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (apiOptions == null)
                throw new ArgumentNullException(nameof(apiOptions));
            if (sessionOptions == null)
                throw new ArgumentNullException(nameof(sessionOptions));

            var session = new SpeechRecognitionSession(
                apiOptions.ApplicationName,
                apiOptions.ApiKey,
                apiOptions.UserId,
                apiOptions.Device,
                sessionOptions,
                apiOptions.Timeout);

            return session.StartAsync(cancellationToken);
        }
    }
}
