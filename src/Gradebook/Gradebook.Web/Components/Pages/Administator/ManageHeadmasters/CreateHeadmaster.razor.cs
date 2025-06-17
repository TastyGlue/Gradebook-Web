namespace Gradebook.Web.Components.Pages.Administator.ManageHeadmasters;

public partial class CreateHeadmaster : ExtendedComponentBase
{
    [Inject] protected IApiHeadmasterService ApiHeadmasterService { get; set; } = default!;
    protected CreateRoleUserViewModel<HeadmasterViewModel> ViewModel { get; set; } = new();

    protected async Task ValidSubmitHandler()
    {
        var dto = ViewModel.Adapt<CreateUserRoleDto<HeadmasterDto>>();
        var result = await ApiHeadmasterService.CreateHeadmaster(dto);

        if (result.Succeeded)
        {
            Notify("Headmaster created successfully", Severity.Success);
            NavigationManager.NavigateTo("/manage-headmasters");
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
        }
    }
}
