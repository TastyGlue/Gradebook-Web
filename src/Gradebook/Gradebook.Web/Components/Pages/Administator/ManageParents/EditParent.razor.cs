namespace Gradebook.Web.Components.Pages.Administator.ManageParents;

public partial class EditParent : ExtendedComponentBase
{
    [Parameter] public Guid Id { get; set; }
    [Inject] protected IApiParentService ApiParentService { get; set; } = default!;
    protected CreateRoleUserViewModel<ParentViewModel> ViewModel { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        PageTitle = "Edit Parent";

        IsLoadingComplete = false;
        LoaderService.ToggleLoading(true);

        var result = await ApiParentService.GetParent(Id);
        if (result.Succeeded)
        {
            ViewModel.Role = result.Value!.Adapt<ParentViewModel>();
            ViewModel.User = result.Value!.User.Adapt<UserViewModel>();
        }
        else
        {
            LoaderService.ToggleLoading(false);
            Notify(result.Error!.Message, Severity.Error);
            NavigationManager.NavigateTo("/manage-parents");
        }

        LoaderService.ToggleLoading(false);
        IsLoadingComplete = true;
    }

    protected async Task ValidSubmitHandler()
    {
        var dto = ViewModel.Adapt<CreateUserRoleDto<ParentDto>>();
        var result = await ApiParentService.EditParent(Id, dto);

        if (result.Succeeded)
        {
            Notify("Parent edited successfully", Severity.Success);
            NavigationManager.NavigateTo("/manage-parents");
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
        }
    }
}