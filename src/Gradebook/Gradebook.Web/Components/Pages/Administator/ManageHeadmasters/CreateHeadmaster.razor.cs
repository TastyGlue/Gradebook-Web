namespace Gradebook.Web.Components.Pages.Administator.ManageHeadmasters;

public partial class CreateHeadmaster : ExtendedComponentBase
{
    protected HeadmasterViewModel _headmaster { get; set; } = new() { IsActive=true};

    protected List<SchoolViewModel> _schools = new();

    [Inject] protected NavigationManager Navigation { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadSchoolsAsync();
    }

    private async Task LoadSchoolsAsync()
    {
        // TODO: Replace with real service call
        await Task.Delay(300);
        _schools = new List<SchoolViewModel>
        {
            new()
            {
                Id = Guid.Parse("f6fd478e-ca69-4572-a1d9-dabd49879aee"),
                Name = "Green Hill School",
                Address = "123 Green St",
                Website = "https://greenhill.edu",
                Headmasters = "Valentin Stonev"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Riverdale High",
                Address = "456 River Rd",
                Website = "https://riverdale.edu",
                Headmasters = "Joseph Santer"
            }
        };
    }

    protected string GetSchoolName(Guid id)
    {
        return _schools.FirstOrDefault(s => s.Id == id)?.Name ?? "Unknown School";
    }

    protected async Task HandleValidSubmit()
    {
        // TODO: Call real backend service
        await Task.Delay(300);
        Snackbar.Add("Headmaster created successfully.", Severity.Success);
        Navigation.NavigateTo("/manage-headmasters");
    }
}
