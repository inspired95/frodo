using System;
using Microsoft.AspNetCore.Mvc;
using TicketResolver.Models;


namespace TicketResolver.Controllers
{
   
    [ApiController]
    [Route("TicketController")]
    public class TicketController : Controller
    {
        public class TicketParameters
        {
            public int UserId { get; set; }
            public Stage[] Stages { get; set; }
        }


        [HttpGet]
        public Ticket[] GetTicketResults(TicketParameters ticketRequest)
        {
           
        }
      

        public class CurrentTicketParameters
        {
            public int UserId { get; set; }
            public DateTime CurrentTime { get; set; }
        }

        [HttpGet]
        public ValidateableTicket GetCurrentTicket(CurrentTicketParameters currentTicketParameters)
        {

        }
    }
}