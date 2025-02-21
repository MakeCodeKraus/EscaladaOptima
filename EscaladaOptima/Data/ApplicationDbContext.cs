using Microsoft.EntityFrameworkCore;
using EscaladaOptima.Models;

namespace EscaladaOptima.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Elemento> Elementos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Elemento>().HasKey(e => e.Id); 
        }
    }
}

