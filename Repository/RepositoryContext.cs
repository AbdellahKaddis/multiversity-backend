using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

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

        modelBuilder.Entity<Faculty>()
            .HasMany(f => f.Departments)
            .WithOne(d => d.Faculty)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Department>()
            .HasMany(d => d.Programs)
            .WithOne(p => p.Department)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Degree>()
            .HasMany(d => d.Programs)
            .WithOne(p => p.Degree)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<University>()
            .HasMany(u => u.Degrees)
            .WithOne(d => d.University)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProgramCourse>()
            .HasKey(pc => pc.Id);

        modelBuilder.Entity<ProgramCourse>()
            .HasOne(pc => pc.Course)
            .WithMany(c => c.ProgramCourses)
            .HasForeignKey(pc => pc.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProgramCourse>()
            .HasOne(pc => pc.AcademicProgram)
            .WithMany(ap => ap.ProgramCourses)
            .HasForeignKey(pc => pc.ProgramId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Faculty>()
            .HasMany(f => f.Courses)
            .WithOne(c => c.Faculty)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Department>()
  .HasMany(d => d.Professors)
  .WithOne(p => p.Department)
  .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProfessorCourse>()
            .HasKey(pc => pc.Id);

        modelBuilder.Entity<ProfessorCourse>()
            .HasOne(pc => pc.Course)
            .WithMany(c => c.ProfessorCourses)
            .HasForeignKey(pc => pc.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProfessorCourse>()
            .HasOne(pc => pc.Professor)
            .WithMany(p => p.ProfessorCourses)
            .HasForeignKey(pc => pc.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    public DbSet<University>? Universities { get; set; }
    public DbSet<Faculty>? Faculties { get; set; }

    public DbSet<Department>? Departments { get; set; }
    public DbSet<Degree>? Degrees { get; set; }
    public DbSet<AcademicProgram>? Programs { get; set; }
    public DbSet<Course>? Courses { get; set; }
    public DbSet<ProgramCourse>? ProgramCourses { get; set; }
    public DbSet<ProfessorCourse>? ProfessorCourses { get; set; }
}
