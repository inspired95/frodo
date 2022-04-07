using System;

namespace FrodoAPI.Domain
{
    public interface ITransportCompany
    {
        Guid Id { get; }

        bool CanGetFromTo(JourneyPoint from, JourneyPoint to);
        double CostFromTo(JourneyPoint from, JourneyPoint to);
        Ticket GetTicket(JourneyPoint from, JourneyPoint to, string passenger);
    }
}