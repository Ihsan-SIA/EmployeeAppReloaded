using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.UserVM
{
    public class EditProfileViewModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
