using Microsoft.EntityFrameworkCore;

namespace VivaVoyages.Models
{
    public class ApplicationDbContext : DbContext
    {
        ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}