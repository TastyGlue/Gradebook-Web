namespace Gradebook.Web.Models.ViewModels
{
    public class GradeViewModel
    {
        public Guid Id { get; set; }

        public decimal Value { get; set; }

        public Guid SchoolYearId { get; set; }

        public SchoolYearViewModel? SchoolYear { get; set; } = default!;

        public DateTime Date { get; set; }

        public Guid SubjectId { get; set; }

        public SubjectViewModel? Subject { get; set; } = default!;

        public Guid StudentId { get; set; }

        public StudentViewModel? Student { get; set; } = default!;

        public Guid? TeacherId { get; set; }

        public TeacherViewModel? Teacher { get; set; }
    }
}
