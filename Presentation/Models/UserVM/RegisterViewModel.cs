using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.UserVM
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; } = default!;
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "passwords do not match")]
        public required string ConfirmPassword { get; init; } = default!;


    }
}
