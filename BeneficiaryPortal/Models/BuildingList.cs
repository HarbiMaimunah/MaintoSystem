using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Models
{
    public class BuildingList
    {
        public int Id { get; set; }
        public char Number { get; set; }
        public bool IsOwned { get; set; }
        public int CityId { get; set; }
        public string? Street { get; set; }

    }
}
