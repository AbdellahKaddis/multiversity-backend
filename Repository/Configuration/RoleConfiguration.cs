

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "UniversityAdmin",
                NormalizedName = "UNIVERSITYADMIN"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "FacultyAdmin",
                NormalizedName = "FACULTYADMIN"
            },
            new IdentityRole
            {
                Id = "3",
                Name = "Professor",
                NormalizedName = "PROFESSOR"
            },
            new IdentityRole
            {
                Id = "4",
                Name = "Student",
                NormalizedName = "STUDENT"
            }
            );
        }
    }
}