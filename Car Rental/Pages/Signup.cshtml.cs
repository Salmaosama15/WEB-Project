using Car_Rental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCrypt.Net;

namespace Car_Rental.Pages
{
    public class SignupModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public User User { get; set; } = new User();

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        // Inject the context through the constructor
        public SignupModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnPost()
        {

          
            if (string.IsNullOrWhiteSpace(User.FirstName) ||
        string.IsNullOrWhiteSpace(User.LastName) ||
        string.IsNullOrWhiteSpace(User.Email) ||
        string.IsNullOrWhiteSpace(User.Password) ||
        string.IsNullOrWhiteSpace(User.ConfirmPassword))
            {
                ErrorMessage = "All fields are required.";
                return Page();
            }


           
            if (User.Password != User.ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();  
            }

           
            User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(User.Password);

            try
            {
                
                if (_context.Users.Any(u => u.Email == User.Email))
                {
                    ErrorMessage = "A user with this email already exists.";
                    return Page();
                }

                _context.Users.Add(User);
                _context.SaveChanges(); 

                SuccessMessage = "Signup successful!";
                return Page(); 
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred: " + ex.Message;
                return Page();  
            }
        }

    }
}
