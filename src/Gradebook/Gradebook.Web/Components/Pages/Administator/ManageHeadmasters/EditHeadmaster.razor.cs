namespace Gradebook.Web.Components.Pages.Administator.ManageHeadmasters;

public partial class EditHeadmaster : ExtendedComponentBase
{
    [Parameter] public Guid Id { get; set; }
    [Inject] protected IApiHeadmasterService ApiHeadmasterService { get; set; } = default!;
    protected CreateRoleUserViewModel<HeadmasterViewModel> ViewModel { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        PageTitle = "Edit Headmaster";

        IsLoadingComplete = false;
        LoaderService.ToggleLoading(true);

        var result = await ApiHeadmasterService.GetHeadmaster(Id);
        if (result.Succeeded)
        {
            ViewModel.Role = result.Value!.Adapt<HeadmasterViewModel>();
            ViewModel.Role.SchoolId = ViewModel.Role.School.Id;
            ViewModel.User = result.Value!.User.Adapt<UserViewModel>();
        }
        else
        {
            LoaderService.ToggleLoading(false);
            Notify(result.Error!.Message, Severity.Error);
            NavigationManager.NavigateTo("/manage-headmasters");
        }

        LoaderService.ToggleLoading(false);
        IsLoadingComplete = true;
    }

    protected async Task ValidSubmitHandler()
    {
        var dto = ViewModel.Adapt<CreateUserRoleDto<HeadmasterDto>>();
        var result = await ApiHeadmasterService.EditHeadmaster(Id, dto);

        if (result.Succeeded)
        {
            Notify("Headmaster edited successfully", Severity.Success);
            NavigationManager.NavigateTo("/manage-headmasters");
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
        }
    }
}
