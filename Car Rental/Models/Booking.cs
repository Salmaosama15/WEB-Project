using System;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }  

        [Required]
        public string Car { get; set; }  

        [Required]
        public DateTime StartDate { get; set; }  

        [Required]
        public DateTime EndDate { get; set; }  

     
        public decimal TotalPrice { get; set; }  

    }
}
