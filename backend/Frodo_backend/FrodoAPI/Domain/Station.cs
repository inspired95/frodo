using System;
using FrodoAPI.Contract;

namespace FrodoAPI.Domain
{
    [Serializable]
    public class Station {
        public string Name { get; set; }
        public GeoCoordinate Coordinate { get; set; }
        public int Id { get; set; }


    }
}