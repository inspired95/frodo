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
        public double Longitude;
        public double Latitude;

        public double DistanceTo(GeoCoordinate other)
        {
            double rlat1 = Math.PI * Latitude / 180;
            double rlat2 = Math.PI * other.Latitude / 180;
            double theta = Longitude - other.Longitude;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515*1.609344;
            return dist;
        }
    }
}
