using System.Collections.Generic;

namespace FrodoAPI.Domain
{
    public class TransportCompany : ITransportCompany
    {
        public string Name;
        public bool IsTaxi;
        public List<Station> ServedStations;
    }
}