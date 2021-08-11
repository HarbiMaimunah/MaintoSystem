using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Models
{
    public class Floor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public char Number { get; set; }

        [Required]
        public int BuildingId { get; set; }
    }
}
