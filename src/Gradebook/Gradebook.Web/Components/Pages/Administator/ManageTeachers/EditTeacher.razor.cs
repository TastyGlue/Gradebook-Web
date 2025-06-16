//namespace Gradebook.Web.Components.Pages.Administator.ManageTeachers
//{
//    public partial class EditTeacher : ExtendedComponentBase
//    {
//        [Parameter] public Guid Id { get; set; }

//        private IEnumerable<SubjectViewModel> _options = new HashSet<SubjectViewModel>();
//        protected TeacherViewModel Model { get; set; } = new();
//        protected SubjectViewModel subject { get; set; } = new();

//        [Inject] protected NavigationManager Navigation { get; set; } = default!;
//        [Inject] protected ISnackbar Snackbar { get; set; } = default!;

//        protected override async Task OnInitializedAsync()
//        {
//            await LoadTeacherAsync();
//            _subjects = GetMockSubjects();
//            _options = Model.Subjects;
//        }

//        private async Task LoadTeacherAsync()
//        {
//            await Task.Delay(200); // Simulate data fetch
//            var teacher = GetMockTeacher();

//            if (teacher != null)
//            {
//                Model = new TeacherViewModel
//                {
//                    Id = teacher.Id,
//                    FullName = teacher.FullName,
//                    Email = teacher.Email,
//                    BusinessEmail = teacher.BusinessEmail,
//                    BusinessPhoneNumber = teacher.BusinessPhoneNumber,
//                    SchoolName = teacher.SchoolName,
//                    ClassName = teacher.ClassName,
//                    Subjects = teacher.Subjects
//                };
//            }
//            else
//            {
//                Snackbar.Add("Teacher not found.", Severity.Error);
//                Navigation.NavigateTo("/manage-teachers");
//            }
//        }

//        protected async Task HandleValidSubmit()
//        {
//            await Task.Delay(300); // Simulate save
//            Snackbar.Add("Teacher updated successfully.", Severity.Success);
//            Navigation.NavigateTo("/manage-teachers");
//        }

//        private List<SubjectViewModel> _subjects =
//        [
//        ];

//        public class Subject(string name) : IEquatable<Subject>
//        {
//            public string Name { get; } = name;

//            public bool Equals(Subject? other)
//            {
//                if (ReferenceEquals(null, other)) return false;
//                if (ReferenceEquals(this, other)) return true;
//                return Name == other.Name;
//            }

//            public override bool Equals(object? obj) => obj is Subject subject && Equals(subject);
//            public override int GetHashCode() => Name.GetHashCode();
//            public override string ToString() => Name;
//        }

//        private List<SubjectViewModel> GetMockSubjects() => new()
//        {
//            new() { Id = Guid.Parse("10000000-0000-0000-0000-000000000001"), Name = "Mathematics" },
//            new() { Id = Guid.Parse("10000000-0000-0000-0000-000000000002"), Name = "Physics" },
//            new() { Id = Guid.Parse("10000000-0000-0000-0000-000000000003"), Name = "Biology" },
//            new() { Id = Guid.Parse("10000000-0000-0000-0000-000000000004"), Name = "Chemistry" }
//        };

//        private TeacherViewModel GetMockTeacher() => new()
//        {
//            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
//            FullName = "Ivan Petrov",
//            Email = "ivan.petrov@school.edu",
//            BusinessEmail = "ivan.p@school.edu",
//            BusinessPhoneNumber = "+359123456789",
//            SchoolName = "Green Hill School",
//            ClassName = "10A",
//            Subjects = new List<SubjectViewModel>
//            {
//                new()
//                {
//                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
//                    Name = "Mathematics"
//                },
//                new()
//                {
//                    Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
//                    Name = "Physics"
//                }
//            }
//        };
//    }
//}
