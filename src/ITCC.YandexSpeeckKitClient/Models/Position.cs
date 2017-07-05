// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace ITCC.YandexSpeeckKitClient.Models
{
    public class Position
    {
        public static readonly Position Zero = new Position(0, 0);

        public double Latitude { get; }
        public double Longitude { get; }

        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString() => $"{Latitude:R},{Longitude:R}";
    }
}
