using static MudBlazor.Colors;

namespace Gradebook.Web.Components.Pages.Administator.ManageTeachers;

public partial class Form : CreateUserRoleBaseComponent<TeacherViewModel>
{
    [Parameter] public bool IsCreate { get; init; } = true;
    [Parameter] public new string PageTitle { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Inject] protected IApiSchoolService ApiSchoolService { get; set; } = default!;
    [Inject] protected IApiSubjectService ApiSubjectService { get; set; } = default!;

    protected IEnumerable<SchoolViewModel> Schools { get; set; } = [];
    protected IEnumerable<SubjectViewModel> AllSubjects { get; set; } = [];
    protected IEnumerable<SubjectViewModel> Subjects { get; set; } = [];
    protected IEnumerable<SubjectViewModel> SelectedSubjects { get; set; } = [];

    protected MudForm FormRef { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        if (IsCreate)
        {
            await LoadUsers();
            await LoadSchools();
        }

        await LoadSubjects();

        if (!IsCreate)
        {
            SelectedSubjects = ViewModel.Role.Subjects.ToList();
        }
    }

    protected async Task SubmitHandler()
    {
        await FormRef.Validate();

        if (FormRef.IsValid)
        {
            ViewModel.Role.SchoolId = ViewModel.Role.School?.Id ?? Guid.Empty;
            ViewModel.Role.Subjects = SelectedSubjects.ToList();
            ViewModel.Role.UserId = ViewModel.User.Id;

            ViewModel.Role.User = null!;
            ViewModel.Role.School = null!;

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

    protected async Task LoadSubjects()
    {
        var result = await ApiSubjectService.GetSubjects();
        if (result.Succeeded)
        {
            AllSubjects = result.Value!.Adapt<List<SubjectViewModel>>();
            
            if (!IsCreate)
            {
                Subjects = result.Value!.Adapt<List<SubjectViewModel>>()
                    .Where(x => x.SchoolId == ViewModel.Role.SchoolId);
                StateHasChanged();
            }
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
        }
    }

    protected void SelectedSchoolChanged(SchoolViewModel value)
    {
        ViewModel.Role.School = value;
        ViewModel.Role.SchoolId = value.Id;
        ViewModel.Role.Subjects.Clear();
        SelectedSubjects = [];
        Subjects = AllSubjects.Where(x => x.SchoolId == value.Id);
        StateHasChanged();
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
        => NavigationManager.NavigateTo("manage-teachers");
}