// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace ITCC.YandexSpeechKitClient.Models
{
    /// <summary>
    /// Coordinates of device.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Zero point (0,0). Uses when coordinates are unknown.
        /// </summary>
        public static readonly Position Zero = new Position(0, 0);

        /// <summary>
        /// Latibude component.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Longitude component.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Creates new position by known coordinates.
        /// </summary>
        /// <param name="latitude">Latitude component, must be in range from -90 to 90.</param>
        /// <param name="longitude">Longitude component, must be in range from -180 to 180.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Position(double latitude, double longitude)
        {
            if (latitude < -90.0 || latitude > 90.0)
                throw new ArgumentOutOfRangeException(nameof(latitude), latitude, "Latitude be in range from -90 to 90.");
            if (longitude < -180.0 || longitude > 180.0)
                throw new ArgumentOutOfRangeException(nameof(longitude), longitude, "Longitude be in range from -180 to 180.");

            Latitude = latitude;
            Longitude = longitude;
        }

        /// <inheritdoc />
        public override string ToString() => $"{Latitude:R},{Longitude:R}";
    }
}
