namespace Gradebook.Web.Models.ViewModels
{
    public class ClassViewModel
    {
        public Guid Id { get; set; }

        public int Year { get; set; }

        public string Signature { get; set; } = default!;

        public Guid SchoolId { get; set; }

        public SchoolViewModel School { get; set; } = default!;

        public Guid? ClassTeacherId { get; set; }

        public TeacherViewModel? ClassTeacher { get; set; }

        public ICollection<StudentViewModel> Students { get; set; } = [];

        public ICollection<TimetableViewModel> Timetables { get; set; } = [];

        public string DisplayName => $"{Year}{Signature}";
    }
}
