using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Models
{
    public class CancellationReason
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ReasonTypeAr { get; set; }

        [Required]
        public string ReasonTypeEn { get; set; }
    }
}
