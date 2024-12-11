﻿using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Models
{
    public class Contact
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
       
    }
}

