using BackEnd;
using Helpers;
using System.ComponentModel.DataAnnotations;

namespace DataContract.GenericDomains
{
    [CollectionName("Users")]
    public class User : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public Roles Role { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
    }
}
