namespace Gradebook.Web.Components.Pages.Administator.ManageTeachers;

public partial class CreateTeacher : ExtendedComponentBase
{
    [Inject] protected IApiTeacherService ApiTeacherService { get; set; } = default!;
    protected CreateRoleUserViewModel<TeacherViewModel> ViewModel { get; set; } = new();

    protected async Task ValidSubmitHandler()
    {
        var dto = ViewModel.Adapt<CreateUserRoleDto<TeacherDto>>();
        var result = await ApiTeacherService.CreateTeacher(dto);

        if (result.Succeeded)
        {
            Notify("Teacher created successfully", Severity.Success);
            NavigationManager.NavigateTo("/manage-teachers");
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
        }
    }
}