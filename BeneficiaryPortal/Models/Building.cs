using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Models
{
    public class Building
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public bool IsOwned { get; set; }

        [Required]
        public int CityId { get; set; }

#nullable enable
        public string? Street { get; set; }

        [Required]
#nullable disable
        public ICollection<Floor> floors { get; set; }

#nullable enable
        public int? BuildingManagerId { get; set; }
    }
}
