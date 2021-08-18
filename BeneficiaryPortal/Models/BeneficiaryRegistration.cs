using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeneficiaryPortal.Models
{
    public class BeneficiaryRegistration
    {

        [Required]
        [RegularExpression(@"^[a-zA-z\s]+$", ErrorMessage = "Accepted characters are alphabets and spaces only")] //Alpha and spaces
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please Enter valid Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Please Enter valid phone number")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[A-Z])(?=.*[@$!%*?&_-])([a-zA-Z0-9@$!%*?&_-]{8,})$", ErrorMessage = "Please Enter valid Password")]
        public string Password { get; set; }

        [Required]
        public int BuildingNumber { get; set; }

        [Required]
        public int FloorNumber { get; set; }

        public List<Building> Buildings { get; set; }

        public List<Floor> Floors { get; set; }
    }
}
