using FrodoAPI.Contract;

namespace FrodoAPI.Domain
{
    public class GeoPoint : JourneyPoint
    {
        public GeoCoordinate Coordinates { get; set; }
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