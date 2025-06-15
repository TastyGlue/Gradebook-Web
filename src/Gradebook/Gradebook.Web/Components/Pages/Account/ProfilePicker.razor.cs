using Gradebook.Shared.Constants;

namespace Gradebook.Web.Components.Pages.Account;

public partial class ProfilePicker : ComponentBase
{
    [Parameter] public List<ProfileClaim> Profiles { get; set; } = [];

    protected override void OnInitialized()
    {
        Profiles = Profiles
            .OrderBy(c => Enum.TryParse<RoleType>(c.RoleType, out var role) ? (int)role : int.MaxValue)
            .ToList();
        StateHasChanged();
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