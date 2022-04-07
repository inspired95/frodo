using FrodoAPI.Contract;

namespace FrodoAPI.Domain
{
    public class Station : JourneyPoint
    {
        public string Name;
        public GeoCoordinate Coordinate;
        public override GeoCoordinate GetCoordinate()
        {
            return Coordinate;
        }

        public override string GetName()
        {
            return Name;
        }
    }
}