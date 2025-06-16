namespace Gradebook.Web.Components.Pages.Administator.ManageHeadmasters;

public partial class Form : ExtendedComponentBase
{
    [Parameter] public HeadmasterViewModel ViewModel { get; set; } = new();
    [Parameter] public new string PageTitle { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }

    [Inject] protected IApiSchoolService ApiSchoolService { get; set; } = default!;

    protected IEnumerable<SchoolViewModel> Schools { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await LoadSchools();
    }

    protected async Task ValidSubmitHandler()
        => await OnValidSubmit.InvokeAsync();

    protected async Task LoadSchools()
    {
        var result = await ApiSchoolService.GetSchools();
        if (result.Succeeded)
        {
            Schools = result.Value!.Adapt<List<SchoolViewModel>>();
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
            NavigationManager.NavigateTo("/");
        }
    }

    protected void CancelHandler()
        => NavigationManager.NavigateTo("manage-headmasters");
}
