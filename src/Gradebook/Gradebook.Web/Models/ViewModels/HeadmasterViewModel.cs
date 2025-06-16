namespace Gradebook.Web.Models.ViewModels;

public class HeadmasterViewModel : ProfileViewModel
{
    [Required]
    [MaxLength(ValidationConstants.TEXT_FIELD_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
    [RegularExpression(Constants.EMAIL_FORMAT_REGEX, ErrorMessage = ValidationConstants.EMAIL)]
    public string BusinessEmail { get; set; } = default!;

    [Required]
    [MaxLength(ValidationConstants.TEXT_FIELD_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
    [RegularExpression(Constants.PHONE_FORMAT_REGEX, ErrorMessage = ValidationConstants.PHONE_NUMBER)]
    public string BusinessPhoneNumber { get; set; } = default!;

    public Guid SchoolId { get; set; }

    public SchoolDto School { get; set; } = default!;

    public bool Equals(HeadmasterViewModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj) => obj is HeadmasterViewModel state && Equals(state);

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => $"{User?.FullName ?? "N/A"} ({BusinessEmail})";
}
