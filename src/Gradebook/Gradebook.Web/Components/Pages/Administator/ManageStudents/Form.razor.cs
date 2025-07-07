namespace Gradebook.Web.Components.Pages.Administator.ManageStudents;

public partial class Form : CreateUserRoleBaseComponent<StudentViewModel>
{
    [Parameter] public bool IsCreate { get; init; } = true;
    [Parameter] public new string PageTitle { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Inject] protected IApiSchoolService ApiSchoolService { get; set; } = default!;
    [Inject] protected IApiClassService ApiClassService { get; set; } = default!;

    protected IEnumerable<SchoolViewModel> Schools { get; set; } = [];
    protected IEnumerable<ClassViewModel> AllClasses { get; set; } = [];
    protected IEnumerable<ClassViewModel> Classes { get; set; } = [];

    protected MudForm FormRef { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        if (IsCreate)
        {
            await LoadUsers();
            await LoadSchools();
        }
        
        await LoadClasses();
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

    protected void SelectedSchoolChanged(SchoolViewModel value)
    {
        ViewModel.Role.School = value;
        ViewModel.Role.SchoolId = value.Id;
        ViewModel.Role.Class = null;
        Classes = AllClasses.Where(x => x.SchoolId == value.Id);
        StateHasChanged();
    }

    protected void SelectedClassChanged(ClassViewModel? value)
    {
        ViewModel.Role.Class = value;
        ViewModel.Role.ClassId = value?.Id ?? null;
    }

    protected async Task LoadClasses()
    {
        var result = await ApiClassService.GetClasses();
        if (result.Succeeded)
        {
            AllClasses = result.Value!.Adapt<IEnumerable<ClassViewModel>>();
            if (!IsCreate)
                Classes = AllClasses.Where(x => x.SchoolId == ViewModel.Role.SchoolId);
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
            NavigationManager.NavigateTo("/");
        }
    }

    protected async Task<IEnumerable<ClassViewModel>> SearchClasses(string searchValue, CancellationToken token)
    {
        if (searchValue is null)
            return Classes;

        await Task.Yield();

        return Classes.Where(x => x.DisplayName.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
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
        => NavigationManager.NavigateTo("manage-students");
}
