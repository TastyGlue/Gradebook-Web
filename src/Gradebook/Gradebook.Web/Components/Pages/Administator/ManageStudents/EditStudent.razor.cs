namespace Gradebook.Web.Components.Pages.Administator.ManageStudents
{
    public partial class EditStudent : ExtendedComponentBase
    {
        [Parameter] public Guid Id { get; set; }
        [Inject] protected IApiStudentService ApiStudentService { get; set; } = default!;
        protected StudentViewModel ViewModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            IsLoadingComplete = false;
            LoaderService.ToggleLoading(true);

            await LoadStudentAsync();

            IsLoadingComplete = true;
            LoaderService.ToggleLoading(false);
        }

        private async Task LoadStudentAsync()
        {
            var result = await ApiStudentService.GetStudent(Id);
            if (!result.Succeeded)
            {
                LoaderService.ToggleLoading(false);
                Notify(result.Error!.Message, Severity.Error);
                NavigationManager.NavigateTo("/manage-students");
                return;
            }

            ViewModel = result.Value!.Adapt<StudentViewModel>();
        }

        protected async Task ValidSubmitHandler()
        {
            var dto = ViewModel.Adapt<StudentDto>();
            var result = await ApiStudentService.EditStudent(Id, dto);

            if (result.Succeeded)
            {
                Notify("Student updated successfully.", Severity.Success);
                NavigationManager.NavigateTo("/manage-students");
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
            }
        }
    }
}