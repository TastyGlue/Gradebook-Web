namespace Gradebook.Web.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    [MaxLength(ValidationConstants.TEXT_FIELD_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH )]
    public string Email { get; set; } = default!;

    [Required]
    [MinLength(8, ErrorMessage = ValidationConstants.MIN_LENGTH )]
    public string Password { get; set; } = default!;
}
