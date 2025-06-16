namespace Gradebook.Web.Models.ViewModels;

public class UserViewModel
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = default!;

    [Required]
    [MaxLength(ValidationConstants.TEXT_FIELD_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
    [RegularExpression(Constants.EMAIL_FORMAT_REGEX, ErrorMessage = ValidationConstants.EMAIL)]
    public string Email { get; set; } = default!;

    [MaxLength(ValidationConstants.TEXT_FIELD_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
    [RegularExpression(Constants.PHONE_FORMAT_REGEX, ErrorMessage = ValidationConstants.PHONE_NUMBER)]
    public string? PhoneNumber { get; set; }

    [Required]
    [MaxLength(ValidationConstants.TEXT_FIELD_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
    public string FullName { get; set; } = default!;

    public bool IsActive { get; set; } = true;

    public ICollection<ProfileDto> Profiles { get; set; } = [];
}
