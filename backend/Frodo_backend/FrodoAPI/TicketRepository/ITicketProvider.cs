using FrodoAPI.Domain;
using FrodoAPI.JourneyRepository;

namespace FrodoAPI.TicketRepository
{
    public interface ITicketProvider
    {
        Ticket GetTicketForStage(JourneyStage stage);
    }

    public class TicketProvider : ITicketProvider
    {
        private readonly ITransportCompanyRepo _transportCompanyRepo;

        public TicketProvider(ITransportCompanyRepo transportCompanyRepo)
        {
            _transportCompanyRepo = transportCompanyRepo;
        }

        public Ticket GetTicketForStage(JourneyStage stage)
        {
            var transportOrganisation = _transportCompanyRepo.Get(stage.TransportCompanyId);
            var ticket = transportOrganisation.GetTicket(stage.From, stage.To, "Bruce Willis");

            ticket.Stage = stage;

            return ticket;
        }
    }

}