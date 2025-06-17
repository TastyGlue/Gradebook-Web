namespace Gradebook.Web.Components.Pages.Administator.ManageTeachers
{
    public partial class EditTeacher : ExtendedComponentBase
    {
        [Parameter] public Guid Id { get; set; }
        [Inject] protected IApiTeacherService ApiTeacherService { get; set; } = default!;
        protected CreateRoleUserViewModel<TeacherViewModel> ViewModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            PageTitle = "Edit Teacher";

            IsLoadingComplete = false;
            LoaderService.ToggleLoading(true);

            var result = await ApiTeacherService.GetTeacher(Id);
            if (result.Succeeded)
            {
                ViewModel.Role = result.Value!.Adapt<TeacherViewModel>();
                ViewModel.Role.SchoolId = ViewModel.Role.School.Id;
                ViewModel.User = result.Value!.User.Adapt<UserViewModel>();
            }
            else
            {
                LoaderService.ToggleLoading(false);
                Notify(result.Error!.Message, Severity.Error);
                NavigationManager.NavigateTo("/manage-teachers");
            }

            LoaderService.ToggleLoading(false);
            IsLoadingComplete = true;
        }

        protected async Task ValidSubmitHandler()
        {
            var dto = ViewModel.Adapt<CreateUserRoleDto<TeacherDto>>();
            var result = await ApiTeacherService.EditTeacher(Id, dto);

            if (result.Succeeded)
            {
                Notify("Teacher edited successfully", Severity.Success);
                NavigationManager.NavigateTo("/manage-teachers");
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
            }
        }
    }
}