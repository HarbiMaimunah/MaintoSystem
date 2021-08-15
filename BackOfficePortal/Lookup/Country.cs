using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackOfficePortal.Lookup
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string CountryNameAr { get; set; }
        
        [Required]
        public string CountryNameEn { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
