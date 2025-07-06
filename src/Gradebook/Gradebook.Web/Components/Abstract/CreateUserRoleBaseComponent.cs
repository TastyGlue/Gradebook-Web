namespace Gradebook.Web.Components.Abstract;

public partial class CreateUserRoleBaseComponent<T> : ExtendedComponentBase where T : ProfileViewModel, new()
{
    [Parameter] public CreateRoleUserViewModel<T> ViewModel { get; set; } = new();
    [Inject] protected IApiUserService ApiUserService { get; set; } = default!;
    protected IEnumerable<UserViewModel> Users { get; set; } = [];

    protected UserCreationType SelectedCreationType { get; set; } = UserCreationType.Existing;

    protected async Task<IEnumerable<UserViewModel>> SearchUsers(string searchValue, CancellationToken token)
    {
        if (searchValue is null)
            return Users;

        await Task.Yield();

        return Users.Where(x => x.ToString().Contains(searchValue, StringComparison.OrdinalIgnoreCase));
    }

    protected async Task LoadUsers()
    {
        var result = await ApiUserService.GetUsers();
        if (result.Succeeded)
        {
            Users = result.Value!.Adapt<List<UserViewModel>>();
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
            NavigationManager.NavigateTo("/");
        }
    }

    protected void UserChanged(UserViewModel value)
    {
        ViewModel.User = value;
        ViewModel.Role.UserId = value.Id;
    }

    protected void SelectedCreationTypeChanged(UserCreationType value)
    {
        SelectedCreationType = value;

        if (value == UserCreationType.Existing)
            ViewModel.User = new();

        ViewModel.FromNewUser = value == UserCreationType.New;
    }

    protected static IEnumerable<string> UserValidity(UserViewModel value)
    {
        if (value is null || value.Id == Guid.Empty)
            yield return "User is required";
    }
}
