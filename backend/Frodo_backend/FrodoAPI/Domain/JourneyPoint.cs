using FrodoAPI.Contract;

namespace FrodoAPI.Domain
{
    public abstract class JourneyPoint
    {
        public abstract GeoCoordinate GetCoordinate();
        public abstract string GetName();
    }
}