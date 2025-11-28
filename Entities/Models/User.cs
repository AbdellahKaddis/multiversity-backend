using Microsoft.AspNetCore.Identity;
using static System.Formats.Asn1.AsnWriter;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "FirstName is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the FirstName is 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the LastName is 50 characters.")]
        public string? LastName { get; set; }

        [MaxLength(10, ErrorMessage = "Maximum length for the Cin is 10 characters.")]
        public string? Cin {  get; set; }

        //[Required(ErrorMessage = "IsActive is a required field.")]
        //public bool IsActive { get; set; }

        public University University { get; set; }
        public Professor Professor {  get; set; }

        public Faculty Faculty { get; set; }
        public User()
        {
            UserName = Email;
        }
    }

}
