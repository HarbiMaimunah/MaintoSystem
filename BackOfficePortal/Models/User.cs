
using BackOfficePortal.Lookup;
using BackOfficePortal.ManyToMany;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackOfficePortal.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-z\s]+$", ErrorMessage = "Accepted characters are alphabets and spaces only")]
        public string Name { get; set; }

        [Required]     
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Please Enter valid phone number")]
        public string Phone { get; set; }

        [Required]      
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please Enter valid Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[A-Z])(?=.*[@$!%*?&_-])([a-zA-Z0-9@$!%*?&_-]{8,})$", ErrorMessage = "Please Enter valid Password")]
        public string Password { get; set; }

        public bool IsForgetPassword { get; set; } = false;

        public bool IsRememberMe { get; set; } = false;

        public bool IsFirstLogin { get; set; } = true;

        //UserRole section
        [ForeignKey("Id")]
        [Required]
        public int UserRoleId { get; set; }

        public UserRole userRole { get; set; }


        #nullable enable
        //user location section
        [ForeignKey("Id")]
        public int? FloorId { get; set; }

        public Floor? floor { get; set; }

        public Building? building { get; set; }

        //JobType section
        [ForeignKey("Id")]
        public int? JobTypeId { get; set; }

        public JobType? jobType { get; set; }


        //Maintenance type section
        [ForeignKey("Id")]
        public int? MaintenanceTypeId { get; set; }

        public MaintenanceType? maintenanceType { get; set; }


        //Ticket relations 
        public ICollection<Ticket>? BeneficiaryTickets { get; set; }
        public ICollection<BackOfficesTickets>? BackOfficeTickets { get; set; }

        public ICollection<Ticket>? TicketsRejected { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
