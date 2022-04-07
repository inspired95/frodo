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
    [Serializable]
    public class JourneyStage
    {
        public JourneyPoint From { get; set; }
        public JourneyPoint To { get; set; }
         public string MeanOfTransportation { get; set; }
        public DateTime StartingTime { get; set; }
        public double Price { get; set; }

        public TimeSpan WaitingTime { get; set; }
        public TimeSpan TravelTime { get; set; }
  
        public Guid TransportCompanyId;
    }

    public class Ticket
    {
        public Guid Id { get; set; }

        public double Price { get; set; }
        public string Product { get; set; }

        public JourneyStage Stage { get; set; }
    }

    public class ValidateableTicket
    {
        public DateTime StartingTime { get; set; }
        public string BarcodeData { get; set; }
        public Guid TicketId { get; set; }
    }

}