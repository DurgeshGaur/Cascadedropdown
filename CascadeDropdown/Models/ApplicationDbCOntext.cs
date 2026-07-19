using Microsoft.EntityFrameworkCore;

namespace CascadeDropdown.Models
{
    public class ApplicationDbCOntext : DbContext
    {
        public ApplicationDbCOntext(DbContextOptions<ApplicationDbCOntext> options ) : base (options) { }

        public DbSet<Country> Country { get; set; } 
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
