using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.UserVM
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; init; } = default!;

        [Required]
        public required string Password { get; init; } = default!;

        public bool RememberMe { get; set; }
    }
}
