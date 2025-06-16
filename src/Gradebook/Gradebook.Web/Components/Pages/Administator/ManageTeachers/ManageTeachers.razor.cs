namespace Gradebook.Web.Components.Pages.Administator.ManageTeachers
{
    public partial class ManageTeachers : ExtendedComponentBase
    {
        [Inject] protected IApiTeacherService ApiTeacherService { get; set; } = default!;
        protected List<TeacherViewModel> _teachers = new();
        protected TeacherViewModel? _selectedTeacher;
        protected string _searchString = string.Empty;
        protected bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            PageTitle = "Manage Teachers";
            await LoadTeachersAsync();
        }

        private async Task LoadTeachersAsync()
        {
            _isLoading = true;
            var result = await ApiTeacherService.GetTeachers();
            if (result.Succeeded)
            {
                _teachers = result.Value!.Adapt<List<TeacherViewModel>>();
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
                NavigationManager.NavigateTo("/");
            }
            _isLoading = false;
        }

        protected void CreateTeacher()
        {
            NavigationManager.NavigateTo("/manage-teachers/create");
        }

        protected void EditTeacher()
        {
            if (_selectedTeacher != null)
                NavigationManager.NavigateTo($"/manage-teachers/edit/{_selectedTeacher.Id}");
        }
    }
}