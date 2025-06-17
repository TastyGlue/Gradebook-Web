namespace Gradebook.Web.Components.Layout
{
    public partial class RoleIcon : ExtendedComponentBase
    {
        [Parameter] public Size size { get; set; }

        protected string GetProfileIcon(RoleType role)
        {
            return role switch
            {
                RoleType.Admin => Icons.Material.Sharp.ManageAccounts,
                RoleType.Headmaster => Icons.Material.Sharp.SupervisorAccount,
                RoleType.Teacher => Icons.Material.Sharp.Badge,
                RoleType.Student => Icons.Material.Sharp.School,
                RoleType.Parent => Icons.Material.Sharp.FamilyRestroom,
                _ => Icons.Material.Sharp.PersonOutline
            };
        }
    }
}
