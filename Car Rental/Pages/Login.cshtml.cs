using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // For session management
using BCrypt.Net;
using System.Linq;

namespace Car_Rental.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnPost()
        {
            // Fetch user from the database
            var user = _context.Users.SingleOrDefault(u => u.Email == Email);
            if (user == null)
            {
                TempData["AlertMessage"] = "Warning: This account does not exist.";
                return Page(); // Reload the page with a warning message
            }

            // Verify the password
            if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                TempData["AlertMessage"] = "Warning: Incorrect email or password.";
                return Page(); // Reload the page with a warning message
            }

            // Login successful - Save user information in session
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.FirstName); // Optional for personalization

            TempData["AlertMessage"] = "Success: Login successful!";
            TempData["RedirectUrl"] = "/carspage"; // URL to redirect after showing success
            return Page(); // Reload the same page to show the alert
        }

    }
}

