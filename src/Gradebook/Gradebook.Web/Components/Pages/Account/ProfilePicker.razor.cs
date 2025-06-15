namespace Gradebook.Web.Components.Pages.Account;

public partial class ProfilePicker : ExtendedComponentBase
{
    [Inject] protected IApiAuthService ApiAuthService { get; set; } = default!;

    [Parameter] public string Token { get; set; } = string.Empty;

    protected List<ProfileClaim> Profiles { get; set; } = [];

    protected override void OnInitialized()
    {
        Profiles = ParseProfilesClaim(Token);

        Profiles = Profiles
            .OrderBy(c => Enum.TryParse<RoleType>(c.RoleType, out var role) ? (int)role : int.MaxValue)
            .ToList();

        StateHasChanged();
    }

    protected async Task ProfileSelectedHandler(ProfileClaim profile)
    {
        var request = new LoginProfile
        {
            ProfileId = profile.ProfileId,
            RoleType = Enum.Parse<RoleType>(profile.RoleType)
        };

        var result = await ApiAuthService.LoginProfile(request, Token);
        if (result.Succeeded)
        {
            // Store the access token in local storage
            await LocalStorage.SetAsync(Constants.ACCESS_TOKEN_KEY, result.Value!);

            NavigationManager.NavigateTo("/");
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
        }
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

    protected string GetProfileIcon(ProfileClaim profile)
    {
        return profile.RoleType switch
        {
            nameof(RoleType.Admin) => Icons.Material.Sharp.ManageAccounts,
            nameof(RoleType.Headmaster) => Icons.Material.Sharp.SupervisorAccount,
            nameof(RoleType.Teacher) => Icons.Material.Sharp.Badge,
            nameof(RoleType.Student) => Icons.Material.Sharp.School,
            nameof(RoleType.Parent) => Icons.Material.Sharp.FamilyRestroom,
            _ => Icons.Material.Sharp.PersonOutline
        };
    }
}