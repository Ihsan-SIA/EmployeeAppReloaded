using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class EditProfileViewModel
{
    public string Id { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string UserName { get; set; } = default!;
    [Required]
    public string PhoneNumber { get; set; } = default!;
}