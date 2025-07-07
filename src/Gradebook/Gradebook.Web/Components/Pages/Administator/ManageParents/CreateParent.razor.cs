namespace Gradebook.Web.Components.Pages.Administator.ManageParents;

public partial class CreateParent : ExtendedComponentBase
{
    [Inject] protected IApiParentService ApiParentService { get; set; } = default!;

    protected CreateRoleUserViewModel<ParentViewModel> ViewModel { get; set; } = new();

    protected async Task ValidSubmitHandler()
    {
        var dto = ViewModel.Adapt<CreateUserRoleDto<ParentDto>>();
        var result = await ApiParentService.CreateParent(dto);

        if (result.Succeeded)
        {
            Notify("Parent created successfully.", Severity.Success);
            NavigationManager.NavigateTo("/manage-parents");
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
        }
    }
}