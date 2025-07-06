namespace Gradebook.Web.Models.ViewModels
{
    public class StudentViewModel : ProfileViewModel
    {
        public Guid SchoolId { get; set; }

        public SchoolViewModel School { get; set; } = default!;

        public Guid? ClassId { get; set; }

        public ClassViewModel? Class { get; set; }

        public ICollection<GradeViewModel> Grades { get; set; } = [];

        public ICollection<AbsenceViewModel> Absences { get; set; } = [];

        public ICollection<ParentViewModel> Parents { get; set; } = [];

        public string ParentsString => (Parents.Count > 0) ? string.Join(", ", Parents.Select(p => p.User?.FullName ?? "")) : string.Empty;

    }
}
