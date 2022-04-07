using System;
using FrodoAPI.Domain;

namespace FrodoAPI.TicketRepository
{
    public interface ITicketRepository 
    {
        ValidateableTicket GetForCurrentStage(in Guid userId, in DateTime currentTime);
        void Add(Guid ticketRequestUserId, in long journeyGuid, Ticket[]	 results);
    }

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
                Price = 10,
                Product = $"Happy Hour {stage.From}",
                Stage = stage
            };
        }
    }

    public class DummyTicketRepository : ITicketRepository
    {
        public ValidateableTicket GetForCurrentStage(in Guid userId, in DateTime currentTime)
        {
            throw new NotImplementedException();
        }

        public void Add(Guid ticketRequestUserId, in long journeyGuid, Ticket[] results)
        {
            throw new NotImplementedException();
        }
    }
}