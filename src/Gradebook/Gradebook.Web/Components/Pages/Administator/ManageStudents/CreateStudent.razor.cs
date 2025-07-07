namespace Gradebook.Web.Components.Pages.Administator.ManageStudents
{
    public partial class CreateStudent : ExtendedComponentBase
    {
        [Inject] protected IApiStudentService ApiStudentService { get; set; } = default!;

        protected CreateRoleUserViewModel<StudentViewModel> ViewModel { get; set; } = new();

        protected async Task ValidSubmitHandler()
        {
            var dto = ViewModel.Adapt<CreateUserRoleDto<StudentDto>>();
            var result = await ApiStudentService.CreateStudent(dto);

            if (result.Succeeded)
            {
                Notify("Student created successfully.", Severity.Success);
                NavigationManager.NavigateTo("/manage-students");
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
            }
        }
    }
}