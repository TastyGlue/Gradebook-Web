namespace Gradebook.Web.Models.ViewModels
{
    public class AbsenceViewModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public bool Excused { get; set; }

        public bool IsLate { get; set; }

        public Guid SchoolYearId { get; set; }

        public SchoolYearViewModel? SchoolYear { get; set; } = default!;

        public Guid StudentId { get; set; }

        public StudentViewModel? Student { get; set; } = default!;

        public Guid TimetableId { get; set; }

        public TimetableViewModel? Timetable { get; set; } = default!;
    }
}
