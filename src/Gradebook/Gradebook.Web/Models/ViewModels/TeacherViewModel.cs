namespace Gradebook.Web.Models.ViewModels
{
    public class TeacherViewModel
    {
        public Guid Id { get; set; }

        // From UserDto
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // From TeacherDto
        public string BusinessEmail { get; set; } = string.Empty;
        public string BusinessPhoneNumber { get; set; } = string.Empty;

        // Display names from navigation properties
        public string SchoolName { get; set; } = string.Empty;
        public string? ClassName { get; set; }

        // Flattened list of subject names
        public List<SubjectViewModel> Subjects { get; set; } = new();
    }
}
