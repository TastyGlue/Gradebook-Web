namespace Gradebook.Web.Models.ViewModels
{
    public class TeacherViewModel : ProfileViewModel
    {
    public string BusinessEmail { get; set; } = default!;

    public string BusinessPhoneNumber { get; set; } = default!;

    public Guid SchoolId { get; set; }

    public SchoolViewModel? School { get; set; } = default!;

    public Guid? ClassId { get; set; }

    public ClassViewModel? Class { get; set; }

    public ICollection<SubjectViewModel>? Subjects { get; set; } = [];

    }
}