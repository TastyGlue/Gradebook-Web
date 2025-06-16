namespace Gradebook.Web.Components.Pages.Administator.ManageStudents
{
    public partial class ManageStudents : ExtendedComponentBase
    {
        [Inject] protected IApiStudentService ApiStudentService { get; set; } = default!;

        protected List<StudentViewModel> _students = new();
        protected StudentViewModel? _selectedStudent;
        protected string _searchString = string.Empty;
        protected bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            PageTitle = "Manage Students";
            await LoadStudentsAsync();
        }

        private async Task LoadStudentsAsync()
        {
            _isLoading = true;
            var result = await ApiStudentService.GetStudents();
            if (result.Succeeded)
            {
                // Adapt DTOs into our viewmodels
                _students = result.Value!
                                  .Adapt<List<StudentViewModel>>();
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
                NavigationManager.NavigateTo("/");
            }
            _isLoading = false;
        }

        protected void CreateStudent()
            => NavigationManager.NavigateTo("/manage-students/create");

        protected void EditStudent()
        {
            if (_selectedStudent != null)
                NavigationManager.NavigateTo($"/manage-students/edit/{_selectedStudent.Id}");
        }

        private bool QuickFilter(StudentViewModel student)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            return student.User.FullName.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
                || student.User.Email.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
                || student.School.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
                || student.ParentsString.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
                || (student.Class?.DisplayName?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true);
        }
    }
}