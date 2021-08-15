using BackOfficePortal.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackOfficePortal.Lookup
{
    public class CancellationReason
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ReasonTypeAr { get; set; }

        [Required]
        public string ReasonTypeEn { get; set; }

        public ICollection<Ticket> tickets { get; set; }
    }
}
