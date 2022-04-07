using FrodoAPI.Contract;

namespace FrodoAPI.Domain
{
    public class GeoPoint : JourneyPoint
    {
        GeoCoordinate Coordinates;
        public override GeoCoordinate GetCoordinate()
        {
            return Coordinates;
        }

        public override string GetName()
        {
            return "";
        }
    }
}