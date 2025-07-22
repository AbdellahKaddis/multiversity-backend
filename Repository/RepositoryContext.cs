using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<University>()
    .HasMany(u => u.Users)
    .WithOne(u => u.University)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<University>()
            .HasIndex(u => u.Name)
            .IsUnique();

            modelBuilder.Entity<University>()
            .HasIndex(u => u.Email)
            .IsUnique();
            modelBuilder.Entity<University>()
.HasIndex(u => u.PhoneNumber)
.IsUnique();
        }
        public DbSet<University>? Universities { get; set; }
    }
}
