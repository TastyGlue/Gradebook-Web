namespace Gradebook.Web.Components.Pages.Administator.ManageHeadmasters;

public partial class Form : CreateUserRoleBaseComponent<HeadmasterViewModel>
{
    [Parameter] public bool IsCreate { get; init; } = true;
    [Parameter] public new string PageTitle { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Inject] protected IApiSchoolService ApiSchoolService { get; set; } = default!;
    
    protected IEnumerable<SchoolViewModel> Schools { get; set; } = [];
    
    protected MudForm FormRef { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        if (IsCreate)
        {
            await LoadUsers();
            await LoadSchools();
        }
    }

    protected async Task SubmitHandler()
    {
        await FormRef.Validate();

        if (FormRef.IsValid)
        {
            ViewModel.Role.SchoolId = ViewModel.Role.School?.Id ?? Guid.Empty;
            await OnValidSubmit.InvokeAsync();
        }
    }

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

    protected async Task<IEnumerable<SchoolViewModel>> SearchSchools(string searchValue, CancellationToken token)
    {
        if (searchValue is null)
            return Schools;

        await Task.Yield();

        return Schools.Where(x => x.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
    }

    protected static IEnumerable<string> SchoolValidity(SchoolViewModel value)
    {
        if (value is null || value.Id == Guid.Empty)
            yield return "School is required";
    }

    protected void CancelHandler()
        => NavigationManager.NavigateTo("manage-headmasters");
}
