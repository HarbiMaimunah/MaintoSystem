using BackOfficePortal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackOfficePortal.Lookup
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string CityNameAr { get; set; }
        
        [Required]
        public string CityNameEn { get; set; }

        public ICollection<Building> buildings { get; set; }

        //country 
        public int CountryId { get; set; }
        public Country country { get; set; }

    }
}
