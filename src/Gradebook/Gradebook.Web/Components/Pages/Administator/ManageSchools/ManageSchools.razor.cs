namespace Gradebook.Web.Components.Pages.Administator.ManageSchools
{
    public partial class ManageSchools : ExtendedComponentBase
    {
        protected List<SchoolViewModel> _schools = new();
        protected SchoolViewModel? _selectedSchool;
        protected string _searchString = "";
        protected bool _isLoading = true;

        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadSchoolsAsync();
        }

        private async Task LoadSchoolsAsync()
        {
            _isLoading = true;
            //dummy data, to be deleted
            // TODO: Replace with real data service call
            await Task.Delay(500);
            _schools = new List<SchoolViewModel>
            {
                new() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Green Hill School", Address = "123 Green St", Website = "https://greenhill.edu", Headmasters = "Valentin Stonev" },
                new() { Id = Guid.NewGuid(), Name = "Riverdale High", Address = "456 River Rd", Website = "https://riverdale.edu", Headmasters = "Joseph Santer"},
            };

            _isLoading = false;
        }

        protected void CreateSchool()
        {
            Navigation.NavigateTo("/manage-schools/create");
        }

        protected void EditSchool()
        {
            if (_selectedSchool != null)
                Navigation.NavigateTo($"/manage-schools/edit/{_selectedSchool.Id}");
        }
    }
}
