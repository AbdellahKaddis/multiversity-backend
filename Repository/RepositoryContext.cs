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
            .HasOne(u => u.Admin)
            .WithOne(u => u.University)
            .HasForeignKey<University>(u => u.AdminId)
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

            modelBuilder.Entity<University>()
                .HasMany(u => u.Faculties)
                .WithOne(f => f.University)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Faculty>()
            .HasOne(f => f.Dean)
            .WithOne(u => u.Faculty)
            .HasForeignKey<Faculty>(f => f.DeanId)
            .OnDelete(DeleteBehavior.Restrict);

        }
        public DbSet<University>? Universities { get; set; }
        public DbSet<Faculty>? Faculties { get; set; }
    }
}
