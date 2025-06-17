namespace Gradebook.Web.Models.ViewModels
{
    public class TeacherViewModel : ProfileViewModel
    {
        [Display(Name = "Business Email")]
        [Required]
        [MaxLength(ValidationConstants.TEXT_FIELD_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
        [RegularExpression(Constants.EMAIL_FORMAT_REGEX, ErrorMessage = ValidationConstants.EMAIL)]
        public string BusinessEmail { get; set; } = default!;

        [Display(Name = "Business Phone Number")]
        [Required]
        [MaxLength(ValidationConstants.TEXT_FIELD_MAX_LENGTH, ErrorMessage = ValidationConstants.MAX_LENGTH)]
        [RegularExpression(Constants.PHONE_FORMAT_REGEX, ErrorMessage = ValidationConstants.PHONE_NUMBER)]
        public string BusinessPhoneNumber { get; set; } = default!;

        public Guid SchoolId { get; set; }

        public SchoolViewModel School { get; set; } = default!;

        public Guid? ClassId { get; set; }

        public ClassViewModel? Class { get; set; }

        public ICollection<SubjectViewModel> Subjects { get; set; } = [];

    }
}