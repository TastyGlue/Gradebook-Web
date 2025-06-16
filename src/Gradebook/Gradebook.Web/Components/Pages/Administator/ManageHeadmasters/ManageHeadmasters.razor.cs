namespace Gradebook.Web.Components.Pages.Administator.ManageHeadmasters;

public partial class ManageHeadmasters : ExtendedComponentBase
{
    [Inject] protected IApiHeadmasterService ApiHeadmasterService { get; set; } = default!;

    protected List<HeadmasterViewModel> Headmasters { get; set; } = [];

    protected HeadmasterViewModel? SelectedHeadmaster { get; set; }
    protected string _searchString = "";
    protected bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        PageTitle = "Manage Headmasters";

        await LoadHeadmastersAsync();
    }

    private async Task LoadHeadmastersAsync()
    {
        _isLoading = true;

        var result = await ApiHeadmasterService.GetHeadmasters();

        if (result.Succeeded)
        {
            Headmasters = result.Value!.Adapt<List<HeadmasterViewModel>>();
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
            NavigationManager.NavigateTo("/");
        }

        _isLoading = false;
    }

    protected void CreateHeadmaster()
    {
        NavigationManager.NavigateTo("/manage-headmasters/create");
    }

    protected void EditHeadmaster()
    {
        if (SelectedHeadmaster != null)
            NavigationManager.NavigateTo($"/manage-headmasters/edit/{SelectedHeadmaster.Id}");
    }

    private Func<HeadmasterViewModel, bool> QuickFilter => x =>
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;

        if (x.User != null && x.User.FullName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.User != null && x.User.Email.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.BusinessEmail.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.BusinessPhoneNumber.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if (x.School != null && x.School.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
}
