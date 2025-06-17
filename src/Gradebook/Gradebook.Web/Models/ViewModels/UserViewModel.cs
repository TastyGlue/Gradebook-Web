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

    [Display(Name = "Full Name")]
    [Required]
    [MaxLength(ValidationConstants.TEXT_FIELD_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
    public string FullName { get; set; } = default!;

    public bool IsActive { get; set; } = true;

    public ICollection<ProfileDto> Profiles { get; set; } = [];

    public bool Equals(UserViewModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj) => obj is UserViewModel state && Equals(state);

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString()
    {
        if (this is null)
            return string.Empty;

        if (FullName is null || Email is null)
            return string.Empty;

        return $"{FullName} ({Email})";
    }
}
