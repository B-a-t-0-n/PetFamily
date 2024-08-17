using Microsoft.EntityFrameworkCore;
using PetFamily.Domain;

namespace PetFamily.Infrastucture
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Volunteer> Volunteers => Set<Volunteer>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
