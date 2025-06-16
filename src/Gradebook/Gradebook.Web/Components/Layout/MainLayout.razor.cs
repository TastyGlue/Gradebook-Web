using System.Security.Claims;

namespace Gradebook.Web.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        private bool isDrawerOpen = true;
      
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; } = default!;

        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        [Inject] public UserStateContainer UserStateContainer { get; set; } = default!;

        [Inject] public LoaderService LoaderService { get; set; } = default!;

        public ClaimsPrincipal User { get; set; } = default!;

        private bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            LoaderService.Register(state =>
            {
                InvokeAsync(() =>
                {
                    IsLoading = state;
                    StateHasChanged();
                });
            });

            User = (await authenticationStateTask).User;

            if (!(User.Identity?.IsAuthenticated ?? false))
            {
                NavigationManager.NavigateTo("/account/login");
                return;
            }

            CreateUserStateContainer();
        }

        private void CreateUserStateContainer()
        {
            if (UserStateContainer.Id == Guid.Empty)
            {
                UserStateContainer.Id = new Guid(User.FindFirstValue(Claims.USER_ID)!);
            }

            if (string.IsNullOrWhiteSpace(UserStateContainer.UserName))
            {
                UserStateContainer.UserName = User.FindFirstValue(Claims.USERNAME)!;
            }

            if (string.IsNullOrWhiteSpace(UserStateContainer.Email))
            {
                UserStateContainer.Email = User.FindFirstValue(Claims.EMAIL)!;
            }

            if (string.IsNullOrWhiteSpace(UserStateContainer.FullName))
            {
                UserStateContainer.FullName = User.FindFirstValue(Claims.FULL_NAME)!;
            }

            if (UserStateContainer.ProfileId == Guid.Empty)
            {
                UserStateContainer.ProfileId = new Guid(User.FindFirstValue(Claims.PROFILE_ID)!);
            }

            if (string.IsNullOrWhiteSpace(UserStateContainer.SchoolName))
            {
                UserStateContainer.SchoolName = User.FindFirstValue(Claims.SCHOOL_NAME);
            }

            if (UserStateContainer.SchoolId is null && UserStateContainer.SchoolId == Guid.Empty)
            {
                var schoolIdClaim = User.FindFirstValue(Claims.SCHOOL_ID);
                UserStateContainer.SchoolId = string.IsNullOrWhiteSpace(schoolIdClaim) ? null : new Guid(schoolIdClaim);
            }

            if (UserStateContainer.ClassId is null && UserStateContainer.ClassId == Guid.Empty)
            {
                var classIdClaim = User.FindFirstValue(Claims.CLASS_ID);
                UserStateContainer.ClassId = string.IsNullOrWhiteSpace(classIdClaim) ? null : new Guid(classIdClaim);
            }

            if (UserStateContainer.Role == 0)
            {
                var role = User.FindFirstValue(Claims.ROLE)!;
                if (Enum.TryParse(role, out RoleType userRole))
                {
                    UserStateContainer.Role = userRole;
                }
                else
                {
                    UserStateContainer.Role = RoleType.Student; // Default role if parsing fails
                }
            }

            UserStateContainer.IsPopulated = true;
        }
        void ToggleDrawer()
        {
            isDrawerOpen = !isDrawerOpen;
        }
    }
}
