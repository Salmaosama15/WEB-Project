using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

public class ContactModel : PageModel
{
    private readonly IConfiguration _configuration;

    public ContactModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public Contact Contact { get; set; }

    public string SuccessMessage { get; set; }
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
        // Initialize if needed
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Please fill in all required fields.";
            return Page();
        }

        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Contacts (Name, Phone, Email, Message) VALUES (@Name, @Phone, @Email, @Message)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", Contact.Name);
                    command.Parameters.AddWithValue("@Phone", Contact.Phone);
                    command.Parameters.AddWithValue("@Email", Contact.Email);
                    command.Parameters.AddWithValue("@Message", Contact.Message);
                    command.ExecuteNonQuery();
                }
            }

            SuccessMessage = "Thank you! We will contact you as soon as possible.";
        }
        catch (Exception ex)
        {
            ErrorMessage = "An error occurred while saving your message. Please try again later.";
        }

        return Page();
    }
}

public class Contact
{
    [Key]
   public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}

