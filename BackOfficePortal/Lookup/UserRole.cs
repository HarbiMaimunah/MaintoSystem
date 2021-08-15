using BackOfficePortal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackOfficePortal.Lookup
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Role { get; set; }

        public ICollection<User> users { get; set; }
    }
}
