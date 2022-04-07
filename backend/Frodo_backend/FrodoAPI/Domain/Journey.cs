using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FrodoAPI.Domain
{
    public class Journey
    {
        public Guid GUID { get; set; }
        public List<JourneyStage> Stages { get; set; } 
    }

    public class JourneyStage
    {
        public JourneyPoint From { get; set; }
        public JourneyPoint To { get; set; }
        DateTime StartingTime { get; set; }
        public TimeSpan WaitingTime;
        public TimeSpan TravelTime;
  
        public long TransportCompanyId;
    }

    public class Ticket
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }
        public string Product { get; set; }

        public JourneyStage Stage { get; set; }
    }

    public class ValidateableTicket
    {
        public Guid UserId { get; set; }
        public string BarcodeData { get; set; }
    }

}