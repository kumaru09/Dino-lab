using System.ComponentModel.DataAnnotations;

namespace dinolab.Models.DTOs.Requests
{
    public class UserRegistrationRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}