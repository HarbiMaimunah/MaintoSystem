using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackOfficePortal.Models
{
    public class AddBuilding
    {
        public char BuildingNumber { get; set; }
        public bool IsOwned { get; set; }
        public string Street { get; set; }
        public int BuildingManagerId { get; set; }

    }
}
