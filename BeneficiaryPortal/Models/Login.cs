using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeneficiaryPortal.Models
{
    public class Login
    {
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please Enter valid Email")]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[A-Z])(?=.*[@$!%*?&_-])([a-zA-Z0-9@$!%*?&_-]{8,})$", ErrorMessage = "Please Enter valid Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
