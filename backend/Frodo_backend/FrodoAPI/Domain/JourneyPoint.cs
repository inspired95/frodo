using System;
using FrodoAPI.Contract;

namespace FrodoAPI.Domain
{
    [Serializable]
    public class JourneyPoint
    {
        public GeoCoordinate Coordinates { get; set; }
  
        public string StopName { get; set; }

        public JourneyPoint(Station station)
        {
            Coordinates = station.Coordinate;
            StopName = station.Name;
        }

        public JourneyPoint()
        {
        }
    }
}