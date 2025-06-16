namespace Gradebook.Web.Components.Pages.Administator.ManageSchools
{
    public partial class ManageSchools : ExtendedComponentBase
    {
        [Inject] protected IApiSchoolService ApiSchoolService { get; set; } = default!;

        protected List<SchoolViewModel> _schools = [];
        protected SchoolViewModel? _selectedSchool;
        protected string _searchString = "";
        protected bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            PageTitle = "Manage Schools";

            await LoadSchools();
        }

        private async Task LoadSchools()
        {
            _isLoading = true;
            
            var result = await ApiSchoolService.GetSchools();

            if (result.Succeeded)
            {
                _schools = result.Value!.Adapt<List<SchoolViewModel>>();
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
                NavigationManager.NavigateTo("/");
            }

            _isLoading = false;
        }

        protected void CreateSchool()
        {
            NavigationManager.NavigateTo("/manage-schools/create");
        }

        protected void EditSchool()
        {
            if (_selectedSchool != null)
                NavigationManager.NavigateTo($"/manage-schools/edit/{_selectedSchool.Id}");
        }
    }
}
