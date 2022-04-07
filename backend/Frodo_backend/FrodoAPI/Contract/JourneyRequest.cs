using System;

namespace FrodoAPI.Contract
{
    public class JourneyRequest
    {
        GeoCoordinate StartingPoint;
        GeoCoordinate EndingPoint;
        DateTime StartingDate;
    }

    public class GeoCoordinate
    {
        double Longitude;
        double Latitude;   
    }
}
