using FrodoAPI.Contract;

namespace FrodoAPI.Domain
{
    public class Station : JourneyPoint
    {
        public string Name;
        public GeoCoordinate Coordinate;

    }
}