using Microsoft.EntityFrameworkCore;
using Rusada.Aviation.Core.Entities;

namespace Rusada.Aviation.Infrastructure.Persistence
{
    public class RusadaDbContext : DbContext
    {
        public RusadaDbContext(DbContextOptions<RusadaDbContext> options)
        : base(options)
        {
        }

        public DbSet<Sighting> Sighting { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sighting>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Sighting>()
                .Property(u => u.Make)
                .HasMaxLength(128);

            modelBuilder.Entity<Sighting>()
                .Property(u => u.Model)
                .HasMaxLength(128);

            modelBuilder.Entity<Sighting>()
                .Property(u => u.Registration)
                .HasMaxLength(8);

            modelBuilder.Entity<Sighting>()
                .Property(u => u.Location)
                .HasMaxLength(255);
        }
    }
}
