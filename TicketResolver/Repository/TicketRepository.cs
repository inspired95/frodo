using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TicketResolver.Models;

namespace TicketResolver.Repository
{
    public class DummyTicketRepository
    {
        public IEnumerable<Ticket> GetAll()
        {
            yield return new Ticket
            {
                
            };
        }
    }

}
