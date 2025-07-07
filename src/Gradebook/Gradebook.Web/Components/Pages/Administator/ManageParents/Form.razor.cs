using MudExtensions;

namespace Gradebook.Web.Components.Pages.Administator.ManageParents;

public partial class Form : CreateUserRoleBaseComponent<ParentViewModel>
{
    [Parameter] public bool IsCreate { get; init; } = true;
    [Parameter] public new string PageTitle { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Inject] protected IApiStudentService ApiStudentService { get; set; } = default!;

    protected ICollection<StudentViewModel> Students { get; set; } = [];
    protected IEnumerable<StudentViewModel> SelectedStudents { get; set; } = [];

    protected MudForm FormRef { get; set; } = default!;
    protected MudSelectExtended<StudentViewModel> StudentSelect { get; set; } = default!;

    protected string? _studentSearchString;

    protected override async Task OnInitializedAsync()
    {
        if (IsCreate)
        {
            await LoadUsers();
        }

        await LoadStudents();

        if (!IsCreate)
        {
            SelectedStudents = ViewModel.Role.Students.ToList();
        }
    }

    protected async Task SubmitHandler()
    {
        await FormRef.Validate();

        if (FormRef.IsValid)
        {
            if (IsCreate)
            {
                if (!ViewModel.FromNewUser && SelectedStudents.Any(x => x.User.Id == ViewModel.User.Id))
                {
                    Notify("You cannot create this Parent because the User is present in the selected Students!", Severity.Error);
                    return;
                }
            }

            ViewModel.Role.Students = SelectedStudents.ToList();
            await OnValidSubmit.InvokeAsync();
        }
    }

    protected async Task LoadStudents()
    {
        var result = await ApiStudentService.GetStudents();
        if (result.Succeeded)
        {
            var students = result.Value!.Adapt<IEnumerable<StudentViewModel>>();
            Students = students.ToList();
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
            NavigationManager.NavigateTo("/");
        }
    }

    protected bool SearchStudents(StudentViewModel value, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;

        if (value.User.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
            || (value.Class is not null && value.Class.DisplayName.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
            return true;

        return false;
    }

    protected string? StudentToString(StudentViewModel student)
        => student?.User.ToString();

    protected void CancelHandler()
        => NavigationManager.NavigateTo("manage-parents");
}
