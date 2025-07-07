namespace Gradebook.Web.Components.Pages.Administator.ManageParents;

public partial class Form : CreateUserRoleBaseComponent<ParentViewModel>
{
    [Parameter] public bool IsCreate { get; init; } = true;
    [Parameter] public new string PageTitle { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Inject] protected IApiStudentService ApiStudentService { get; set; } = default!;

    protected IEnumerable<StudentViewModel> Students { get; set; } = [];
    protected IEnumerable<StudentViewModel> FilteredStudents { get; set; } = [];
    protected IEnumerable<StudentViewModel> SelectedStudents { get; set; } = [];

    protected MudForm FormRef { get; set; } = default!;

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
            await OnValidSubmit.InvokeAsync();
        }
    }

    protected async Task LoadStudents()
    {
        var result = await ApiStudentService.GetStudents();
        if (result.Succeeded)
        {
            var students = result.Value!.Adapt<IEnumerable<StudentViewModel>>();
            Students = students;
            FilteredStudents = students;
        }
        else
        {
            Notify(result.Error!.Message, Severity.Error);
            NavigationManager.NavigateTo("/");
        }
    }

    protected void CancelHandler()
        => NavigationManager.NavigateTo("manage-parents");
}
