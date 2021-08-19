using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Models
{
    public class TicketRequest
    {
        
        [Required(ErrorMessage = "Please enter a description of the problem")]
        [RegularExpression(@"^[a-zA-z\s]+$", ErrorMessage = "Accepted characters are alphabets and spaces only")] //Alpha and spaces
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the floor number")]
        public int FloorId { get; set; }

        [Required(ErrorMessage = "Please choose a type of maintenance")]
        public int MaintenanceTypeID { get; set; }

        [Required(ErrorMessage = "Please enter date and time for the maintenance")]
        public DateTime Date { get; set; }

#nullable enable
        [RegularExpression(@"([0-9a-zA-Z\._-]+.(png|PNG|gif|GIF|jp[e]?g|JP[E]?G))", ErrorMessage = "Accepted file type is image only")] //Image file names
        public string? Picture { get; set; }

        public TicketRequest()
        {
            MaintenanceTypeID = 0;
        }
    }
}
