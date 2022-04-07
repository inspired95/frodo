using System;
using FrodoAPI.Domain;

namespace FrodoAPI.TicketRepository
{
    public interface ITicketProvider
    {
        Ticket GetTicketForStage(JourneyStage stage);
    }

    public class DummyTicketProvider : ITicketProvider
    {
        public Ticket GetTicketForStage(JourneyStage stage)
        {
            return new Ticket
            {
                Id = Guid.NewGuid(),
                Price = 10,
                Product = $"Happy Hour {stage.From} with {stage.TransportCompanyId}",
                Stage = stage
            };
        }
    }

}