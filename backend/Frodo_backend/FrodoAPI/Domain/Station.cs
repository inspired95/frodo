using System;
using FrodoAPI.Contract;

namespace FrodoAPI.Domain
{
    [Serializable]
    public class Station : JourneyPoint
    {
        public string Name { get; set; }
        public GeoCoordinate Coordinate { get; set; }
        public int Id { get; set; }

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