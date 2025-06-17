namespace Gradebook.Web.Models.ViewModels
{
    public class TimetableViewModel
    {
        public Guid Id { get; set; }

        public Guid SchoolYearId { get; set; }

        public SchoolYearViewModel SchoolYear { get; set; } = default!;

        public DateTime TimeOfDay { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public Guid? TeacherId { get; set; }

        public TeacherViewModel? Teacher { get; set; }

        public Guid? ClassId { get; set; }

        public ClassViewModel? Class { get; set; }

        public Guid SubjectId { get; set; }

        public SubjectViewModel Subject { get; set; } = default!;

    }
}
