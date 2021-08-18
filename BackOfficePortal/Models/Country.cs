using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackOfficePortal.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string CountryNameAr { get; set; }

        public string CountryNameEn { get; set; }
        public ICollection<City> Cities { get; set; }

    }
}
