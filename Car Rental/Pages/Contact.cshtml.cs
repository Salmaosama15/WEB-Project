using Car_Rental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

public class ContactModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ContactModel> _logger;

    // Inject the ApplicationDbContext and Logger
    public ContactModel(ApplicationDbContext context, ILogger<ContactModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Model to bind the contact form
    [BindProperty]
    public Contact Contact { get; set; }

    // Property to hold error messages
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        try
        {
            // Basic validation: Ensure fields are not empty or whitespace
            if (string.IsNullOrWhiteSpace(Contact.Name) ||
                string.IsNullOrWhiteSpace(Contact.Phone) ||
                string.IsNullOrWhiteSpace(Contact.Email) ||
                string.IsNullOrWhiteSpace(Contact.Message))
            {
                ErrorMessage = "All fields are required.";
                return Page();
            }

            // Email validation using a simple regex pattern (for demonstration purposes)
            if (!IsValidEmail(Contact.Email))
            {
                ErrorMessage = "Please enter a valid email address.";
                return Page();
            }

            // Phone number validation (basic length check, can be expanded)
            if (Contact.Phone.Length < 10)
            {
                ErrorMessage = "Please enter a valid phone number.";
                return Page();
            }

            // Add the contact form data to the context
            _context.Contact.Add(Contact);

            // Save changes to the database
            _context.SaveChanges();

            // Redirect to a confirmation page or a success message
            return RedirectToPage("/ContactConfirmation");
        }
        catch (Exception ex)
        {
            // Log the full exception details
            _logger.LogError(ex, "Error saving contact form");

            // Add a general error message
            ErrorMessage = "An error occurred while saving your contact information.";
            return Page(); // Return to the page with the error message
        }
    }

    // Helper method for simple email validation
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

}




