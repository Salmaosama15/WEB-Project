using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; 
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
            var user = _context.Users.SingleOrDefault(u => u.Email == Email);
            if (user == null)
            {
                TempData["AlertMessage"] = "Warning: This account does not exist.";
                return Page(); 
            }

            
            if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                TempData["AlertMessage"] = "Warning: Incorrect email or password.";
                return Page(); 
            }

           
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.FirstName); 

            TempData["AlertMessage"] = "Success: Login successful!";
            TempData["RedirectUrl"] = "/carspage"; 
            return Page(); 
        }

    }
}

