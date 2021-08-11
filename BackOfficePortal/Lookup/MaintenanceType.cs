using BackOfficePortal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackOfficePortal.Lookup
{
    public class MaintenanceType
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string MaintenanceTypeNameAr { get; set; }
        
        [Required]
        public string MaintenanceTypeNameEn { get; set; }

        public ICollection<Ticket> tickets { get; set; }
        public ICollection<User> users { get; set; }
    }
}
