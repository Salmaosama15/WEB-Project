using Microsoft.AspNetCore.Mvc;
using Car_Rental.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental.Pages
{
    public class BookingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BookingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public void OnGet()
        {
            // Handle GET request (if needed)
        }

        [HttpPost]
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Save the booking to the database
                _context.Bookings.Add(Booking);
                _context.SaveChanges();

                // Set success message using TempData
                TempData["SuccessMessage"] = "Success!Your booking has been confirmed. Our team will contact you shortly with further details. Thank you for choosing us!";
                return Page(); // This reloads the page to display the success message
            }

            // If the model is not valid, set an error message
            TempData["ErrorMessage"] = "Please fill in all required fields!";
            return Page();
        }

    }

}
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