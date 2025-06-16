namespace Gradebook.Web.Components.Pages.Administator.ManageSchools
{
    public partial class EditSchool : ExtendedComponentBase
    {
        [Parameter] public Guid Id { get; set; }
        [Inject] protected IApiHeadmasterService ApiHeadmasterService { get; set; } = default!;
        [Inject] protected IApiSchoolService ApiSchoolService { get; set; } = default!;

        protected SchoolViewModel ViewModel { get; set; } = new();
        protected IEnumerable<HeadmasterViewModel> Headmasters { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
            IsLoadingComplete = false;
            LoaderService.ToggleLoading(true);

            await GetSchool();
            await LoadHeadmasters();

            IsLoadingComplete = true;
            LoaderService.ToggleLoading(false);
        }

        protected async Task GetSchool()
        {
            var result = await ApiSchoolService.GetSchool(Id);

            if (!result.Succeeded)
            {
                LoaderService.ToggleLoading(false);

                Notify(result.Error!.Message, Severity.Error);

                NavigationManager.NavigateTo("manage-schools");
                return;
            }
            else
            {
                ViewModel = result.Value!.Adapt<SchoolViewModel>();
            }
        }

        protected async Task LoadHeadmasters()
        {
            var result = await ApiHeadmasterService.GetHeadmasters();

            if (!result.Succeeded)
            {
                LoaderService.ToggleLoading(false);

                Notify(result.Error!.Message, Severity.Error);

                NavigationManager.NavigateTo("manage-schools");
                return;
            }
            else
            {
                Headmasters = result.Value!.Adapt<IEnumerable<HeadmasterViewModel>>();
            }
        }

        protected async Task ValidSubmitHandler()
        {
            var result = await ApiSchoolService.EditSchool(Id, ViewModel.Adapt<SchoolDto>());

            if (result.Succeeded)
            {
                Notify("Operation succeeded.", Severity.Success);
                NavigationManager.NavigateTo("manage-schools");
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
            }
        }
    }
}
