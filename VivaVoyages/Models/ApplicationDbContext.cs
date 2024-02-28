using Microsoft.EntityFrameworkCore;


namespace VivaVoyages.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}