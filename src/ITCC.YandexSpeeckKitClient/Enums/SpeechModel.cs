// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using ITCC.YandexSpeeckKitClient.Attributes;

namespace ITCC.YandexSpeeckKitClient.Enums
{
    /// <summary>
    /// Language model used for recognition.
    /// </summary>
    public enum SpeechModel
    {
        /// <summary>
        /// Phrases (3-5 words) on different topics, including queries in search engines (on websites).
        /// </summary>
        [EnumValueString("queries")]
        Queries,
        
        /// <summary>
        /// Addresses and names of businesses and geographical features.
        /// </summary>
        [EnumValueString("maps")]
        Maps,
        
        /// <summary>
        /// Names of months, ordinal numbers, and cardinal numbers.
        /// </summary>
        [EnumValueString("dates")]
        Dates,
        
        /// <summary>
        /// First names and last names and requests to connect to someone on the phone.
        /// </summary>
        [EnumValueString("names")]
        Names,
        
        /// <summary>
        /// Cardinal numbers from 1 to 999 and separators — comma, dot, and dash. The model is for dictating phone numbers, account numbers, or document numbers.
        /// </summary>
        [EnumValueString("numbers")]
        Numbers,
        
        /// <summary>
        /// Titles of musical works and names of artists. This model is not intended for recognizing fragments of music. It is only for recognizing names of songs and performers.
        /// </summary>
        [EnumValueString("music")]
        Music,
        
        /// <summary>
        /// Phrases related to making orders in online stores (confirming an order and the delivery method).
        /// </summary>
        [EnumValueString("buying")]
        Buying
    }
}