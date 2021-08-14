using BackOfficePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficePortal.ManyToMany
{
    public class UserTickets
    {
        public int TicketId { get; set; }
        public Ticket ticket { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }
    }
}
