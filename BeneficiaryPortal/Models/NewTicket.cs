using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Models
{
    public class NewTicket
    {
        public TicketRequest Ticket { get; set; }

        [BindProperty]
        public IFormFile Attachment { get; set; }

        public NewTicket()
        {
            Ticket = new TicketRequest() {
                Description = ""
            };
        }

    }
}
