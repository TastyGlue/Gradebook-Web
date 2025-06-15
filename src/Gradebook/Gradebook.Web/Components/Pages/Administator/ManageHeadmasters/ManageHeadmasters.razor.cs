namespace Gradebook.Web.Components.Pages.Administator.ManageHeadmasters;

public partial class ManageHeadmasters : ExtendedComponentBase
{
    protected List<HeadmasterViewModel> _headmasters = new();
    protected List<SchoolViewModel> _schools = new();
    protected HeadmasterViewModel? _selectedHeadmaster;
    protected string _searchString = "";
    protected bool _isLoading = true;

    [Inject] protected NavigationManager Navigation { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadSchoolsAsync();
        await LoadHeadmastersAsync();
    }

    private async Task LoadSchoolsAsync()
    {
        // Simulate data retrieval
        await Task.Delay(200);
        _schools = new List<SchoolViewModel>
        {
            new() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Green Hill School", Address = "123 Green St", Website = "https://greenhill.edu", Headmasters = "Valentin Stonev" },
            new() { Id = Guid.NewGuid(), Name = "Riverdale High", Address = "456 River Rd", Website = "https://riverdale.edu", Headmasters = "Joseph Santer" }
        };
    }

    private async Task LoadHeadmastersAsync()
    {
        _isLoading = true;
        await Task.Delay(300);

        _headmasters = new List<HeadmasterViewModel>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FullName = "Valentin Stonev",
                Email = "valentin.stonev@school.edu",
                BusinessEmail = "valentin.s@school.edu",
                BusinessPhoneNumber = "+359111111111",
                SchoolId = _schools[0].Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                FullName = "Joseph Santer",
                Email = "joseph.santer@school.edu",
                BusinessEmail = "joseph.s@school.edu",
                BusinessPhoneNumber = "+359222222222",
                SchoolId = _schools[1].Id
            }
        };

        _isLoading = false;
    }

    protected void CreateHeadmaster()
    {
        Navigation.NavigateTo("/manage-headmasters/create");
    }

    protected void EditHeadmaster()
    {
        if (_selectedHeadmaster != null)
            Navigation.NavigateTo($"/manage-headmasters/edit/{_selectedHeadmaster.Id}");
    }

    private Func<HeadmasterViewModel, bool> QuickFilter => x =>
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;

        if (x.FullName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.Email.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.BusinessEmail.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.BusinessPhoneNumber.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        var school = _schools.FirstOrDefault(s => s.Id == x.SchoolId);
        if (school != null && school.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
}
