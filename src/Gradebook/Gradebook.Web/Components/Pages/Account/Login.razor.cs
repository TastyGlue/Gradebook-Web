namespace Gradebook.Web.Components.Pages.Account;

public partial class Login : ExtendedComponentBase
{
    [Inject] public IApiAuthService ApiAuthService { get; set; } = default!;

    protected LoginViewModel ViewModel { get; set; } = new();

    bool IsPasswordVisible = false;
    InputType PasswordInputType = InputType.Password;
    string PasswordIcon = Icons.Material.Sharp.VisibilityOff;

    protected async Task ValidSubmitHandler()
    {
        LoaderService.ToggleLoading(true);

        var result = await ApiAuthService.LoginWithCredentials(ViewModel.Adapt<LoginCredentials>());

        if (result.Succeeded)
            Notify(result.Value!, Severity.Success);
        else
            Notify(result.Error!.Message, Severity.Error);

        LoaderService.ToggleLoading(false);
    }

    private void PasswordVisibilityHandler()
    {
        if (IsPasswordVisible)
        {
            PasswordIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInputType = InputType.Password;
        }
        else
        {
            PasswordIcon = Icons.Material.Filled.Visibility;
            PasswordInputType = InputType.Text;
        }

        IsPasswordVisible = !IsPasswordVisible;
    }
}
