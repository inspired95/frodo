using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FrodoAPI.Domain
{
    public class Journey
    {
        long GUID { get; set; }
        List<JourneyStage> Stages { get; set; } 
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
}