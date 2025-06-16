namespace Gradebook.Web.Components.Pages.Administator.ManageTeachers
{
    public partial class ManageTeachers : ExtendedComponentBase
    {
        protected List<TeacherViewModel> _teachers = new();
        protected TeacherViewModel? _selectedTeacher;
        protected string _searchString = string.Empty;
        protected bool _isLoading = true;

        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadTeachersAsync();
        }

        private async Task LoadTeachersAsync()
        {
            _isLoading = true;
            // TODO: Replace with real service call
            await Task.Delay(500);

            _teachers = new List<TeacherViewModel>
            {
                new TeacherViewModel
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    FullName = "Ivan Petrov",
                    Email = "ivan.petrov@school.edu",
                    BusinessEmail = "ivan.p@school.edu",
                    BusinessPhoneNumber = "+359123456789",
                    SchoolName = "Green Hill School",
                    ClassName = "10A",
                    Subjects = new List<SubjectViewModel>
                    {
                        new SubjectViewModel
                        {
                            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                            Name = "Maths"
                        },
                        new SubjectViewModel
                        {
                            Id = Guid.Parse("baaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                            Name = "Biology"
                        }
                    }
                },
                new TeacherViewModel
                {
                    Id = Guid.NewGuid(),
                    FullName = "Maria Georgieva",
                    Email = "maria.georgieva@school.edu",
                    BusinessEmail = "maria.g@school.edu",
                    BusinessPhoneNumber = "+359987654321",
                    SchoolName = "Riverdale High",
                    ClassName = "11B",
                    Subjects = new List<SubjectViewModel>
                    {
                        new SubjectViewModel
                        {
                            Id = Guid.Parse("baaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                            Name = "Physics"
                        }
                    }
                }
            };

            _isLoading = false;
        }

        protected void CreateTeacher()
        {
            Navigation.NavigateTo("/manage-teachers/create");
        }

        protected void EditTeacher()
        {
            if (_selectedTeacher != null)
                Navigation.NavigateTo($"/manage-teachers/edit/{_selectedTeacher.Id}");
        }

        private Func<TeacherViewModel, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            return new[] { x.FullName, x.Email, x.BusinessEmail, x.BusinessPhoneNumber, x.SchoolName }
                   .Any(field => field?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true);
        };
    }
}