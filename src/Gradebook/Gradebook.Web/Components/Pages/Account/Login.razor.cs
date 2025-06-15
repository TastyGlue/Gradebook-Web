namespace Gradebook.Web.Components.Pages.Account;

public partial class Login : ExtendedComponentBase
{
    [Inject] public IApiAuthService ApiAuthService { get; set; } = default!;

    protected LoginViewModel ViewModel { get; set; } = new();

    protected List<ProfileClaim> UserProfiles { get; set; } = [];

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
            UserProfiles = ParseProfilesClaim(result.Value!);
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

    protected List<ProfileClaim> ParseProfilesClaim(string token)
    {
        var claims = TokenUtils.ParseClaimsFromToken(token);

        var profilesClaimValue = claims.Where(c => c.Type == Claims.PROFILES).Select(c => c.Value).First();

        var profiles = JsonSerializer.Deserialize<List<ProfileClaim>>(profilesClaimValue, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return profiles!;
    }
}
