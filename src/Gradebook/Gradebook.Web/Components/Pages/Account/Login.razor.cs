namespace Gradebook.Web.Components.Pages.Account;

public partial class Login : ExtendedComponentBase
{
    [Inject] public IApiAuthService ApiAuthService { get; set; } = default!;

    protected LoginViewModel ViewModel { get; set; } = new();

    protected string AuthToken { get; set; } = default!;

    public string ErrorMessage { get; set; } = string.Empty;

    protected bool IsLoginComplete { get; set; } = false;

    bool IsPasswordVisible = false;
    InputType PasswordInputType = InputType.Password;
    string PasswordIcon = Icons.Material.Sharp.VisibilityOff;

    protected async Task ValidSubmitHandler()
    {
        ErrorMessage = string.Empty;

        LoaderService.ToggleLoading(true);

        var result = await ApiAuthService.LoginWithCredentials(ViewModel.Adapt<LoginCredentials>());

        if (result.Succeeded)
        {
            AuthToken = result.Value!;
            IsLoginComplete = true;
        }
        else
            ErrorMessage = result.Error!.Message;

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
