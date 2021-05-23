using System.ComponentModel.DataAnnotations;

namespace Dinolab.Models.DTOs.Requests
{
    public class UserRegistrationRequestDTO
    {

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        [RegularExpression(@"^\w{3,}$", ErrorMessage = "The field Username must have more than three and not have special character")]
        public string Username { get; set; }
    }
}