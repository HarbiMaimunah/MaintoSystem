using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Models
{
    public class MaintenanceType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MaintenanceTypeNameAr { get; set; }

        [Required]
        public string MaintenanceTypeNameEn { get; set; }
    }
}
