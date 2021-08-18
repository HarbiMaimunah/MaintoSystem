using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StatusTypeAr { get; set; }

        [Required]
        [RegularExpression(@"^[[:alpha:]\s]+$", ErrorMessage = "Accepted characters are alphabets and spaces only")] //Alpha and spaces
        public string StatusTypeEn { get; set; }
    }
}
