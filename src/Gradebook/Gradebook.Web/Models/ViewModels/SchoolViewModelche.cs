namespace Gradebook.Web.Models.ViewModels;

public class SchoolViewModelche
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.TEXT_FIELD_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
    public string Name { get; set; } = default!;

    [Required]
    [MaxLength(ValidationConstants.TEXT_FIELD_LARGE_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
    public string Address { get; set; } = default!;

    [MaxLength(ValidationConstants.TEXT_FIELD_LARGE_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
    [RegularExpression(Constants.WEBSITE_FORMAT_REGEX, ErrorMessage = ValidationConstants.WEBSITE)]
    public string? Website { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<HeadmasterViewModelche> Headmasters { get; set; } = [];

    public ICollection<SchoolYearDto> SchoolYears { get; set; } = [];
}
