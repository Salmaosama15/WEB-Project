using System.ComponentModel.DataAnnotations;
using Car_Rental.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().HasKey(c => c.Id); // Explicitly define the primary key

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Booking> Bookings { get; set; }

}






