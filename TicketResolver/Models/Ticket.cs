using System;

namespace TicketResolver.Models
{
    public class Ticket : Stage
    {
        public decimal Price { get; set; }
        public string Product { get; set; }
    }

    public class Stage
    {
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public string TransportOrganization { get; set; }
        public string Departure

    }


    public class ValidateableTicket
    {
        public int UserId { get; set; }
        public string BarcodeData { get; set; }
    }


}