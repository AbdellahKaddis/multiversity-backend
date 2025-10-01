using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UniversityAdminForRegistrationDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(50, ErrorMessage = "Maximum length for the FirstName is 50 characters.")]
        public string? FirstName { get; init; }

        [Required(ErrorMessage = "LastName is required")]
        [MaxLength(50, ErrorMessage = "Maximum length for the LastName is 50 characters.")]
        public string? LastName { get; init; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }

    }

}
