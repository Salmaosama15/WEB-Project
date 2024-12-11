using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        // Navigation property to related contacts
     
    }
}



