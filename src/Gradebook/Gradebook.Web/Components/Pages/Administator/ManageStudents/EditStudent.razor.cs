namespace Gradebook.Web.Components.Pages.Administator.ManageStudents
{
    public partial class EditStudent : ExtendedComponentBase
    {
        [Parameter] public Guid Id { get; set; }
        [Inject] protected IApiStudentService ApiStudentService { get; set; } = default!;
        protected CreateRoleUserViewModel<StudentViewModel> ViewModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            PageTitle = "Edit Student";

            IsLoadingComplete = false;
            LoaderService.ToggleLoading(true);

            var result = await ApiStudentService.GetStudent(Id);
            if (result.Succeeded)
            {
                ViewModel.Role = result.Value!.Adapt<StudentViewModel>();
                ViewModel.Role.SchoolId = ViewModel.Role.School.Id;
                ViewModel.User = result.Value!.User.Adapt<UserViewModel>();
            }
            else
            {
                LoaderService.ToggleLoading(false);
                Notify(result.Error!.Message, Severity.Error);
                NavigationManager.NavigateTo("/manage-students");
            }

            LoaderService.ToggleLoading(false);
            IsLoadingComplete = true;
        }

        protected async Task ValidSubmitHandler()
        {
            var dto = ViewModel.Adapt<CreateUserRoleDto<StudentDto>>();
            var result = await ApiStudentService.EditStudent(Id, dto);

            if (result.Succeeded)
            {
                Notify("Headmaster edited successfully", Severity.Success);
                NavigationManager.NavigateTo("/manage-students");
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
            }
        }
    }
}