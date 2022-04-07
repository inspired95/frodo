using System;
using System.Linq;
using FrodoAPI.Domain;
using FrodoAPI.JourneyRepository;
using FrodoAPI.TicketRepository;
using FrodoAPI.UserRepository;
using Microsoft.AspNetCore.Mvc;

namespace FrodoAPI.Controllers
{
    [ApiController]
    [Route("TicketController")]
    public class TicketController : Controller
    {
        private readonly ITicketProvider _ticketProvider;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJourneyRepository _journeyRepository;

        public TicketController(ITicketProvider ticketProvider, ITicketRepository ticketRepository, IUserRepository userRepository, IJourneyRepository journeyRepository)
        {
            _ticketProvider = ticketProvider;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _journeyRepository = journeyRepository;
        }

        public class TicketParameters
        {
            public Guid UserId { get; set; }
            public Guid Journey { get; set; }
        }

        [HttpPost]
        public Ticket[] GetPossibleTickets(TicketParameters ticketRequest)
        {
            var journey = _journeyRepository.GetJourney(ticketRequest.Journey);

            var results = journey.Stages.Select(s => _ticketProvider.GetTicketForStage(s)).ToArray();


            return results;
        }

        [HttpPost]
        public void BuyTickets(Ticket[] tickets)
        {

        }


        public class CurrentTicketParameters
        {
            public Guid UserId { get; set; }
            public DateTime CurrentTime { get; set; }
        }

        [HttpPost]
        public ValidateableTicket GetCurrentTicket(CurrentTicketParameters currentTicketParameters)
        {
            return _ticketRepository.GetForCurrentStage(currentTicketParameters.UserId, currentTicketParameters.CurrentTime);
        }

    }
}