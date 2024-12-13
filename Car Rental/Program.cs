using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Car_Rental.Models; // Your ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session and cookie services
builder.Services.AddDistributedMemoryCache(); // Memory cache for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Cookie settings
    options.Cookie.IsEssential = true; // Essential for session functionality
});

// Add cookie-based authentication
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Use authentication and session middleware
app.UseAuthentication(); // Make sure this comes before UseAuthorization()
app.UseSession(); // Make sure to add this line for session management

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map Razor Pages
app.MapRazorPages();

app.Run();
