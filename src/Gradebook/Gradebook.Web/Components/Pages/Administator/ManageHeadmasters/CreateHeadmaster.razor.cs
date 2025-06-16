namespace Gradebook.Web.Components.Pages.Administator.ManageHeadmasters;

public partial class CreateHeadmaster : ExtendedComponentBase
{
    protected HeadmasterViewModel ViewModel { get; set; } = new();

    protected List<SchoolViewModel> Schools { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await LoadSchools();
    }

    private async Task LoadSchools()
    {
        
    }

    protected async Task HandleValidSubmit()
    {
        // TODO: Call real backend service
        await Task.Delay(300);
        Snackbar.Add("Headmaster created successfully.", Severity.Success);
        //Navigation.NavigateTo("/manage-headmasters");
    }
}
