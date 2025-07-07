using System.Xml.Linq;

namespace Gradebook.Web.Models.ViewModels
{
    public class StudentViewModel : ProfileViewModel, IEquatable<StudentViewModel>
    {
        public Guid SchoolId { get; set; }

        public SchoolViewModel School { get; set; } = default!;

        public Guid? ClassId { get; set; }

        public ClassViewModel? Class { get; set; }

        public ICollection<GradeViewModel> Grades { get; set; } = [];

        public ICollection<AbsenceViewModel> Absences { get; set; } = [];

        public ICollection<ParentViewModel> Parents { get; set; } = [];

        public string ParentsString => (Parents.Count > 0) ? string.Join(", ", Parents.Select(p => p.User?.FullName ?? "")) : string.Empty;

        public bool Equals(StudentViewModel? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj) => obj is StudentViewModel student && Equals(student);
        public override int GetHashCode() => Id.GetHashCode();

    }
}
