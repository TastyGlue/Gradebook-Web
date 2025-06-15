using MudBlazor;

namespace Gradebook.Web.Components.Pages.Administator.ManageTeachers;

public partial class ManageTeachers : ExtendedComponentBase
{
    protected List<TeacherViewModel> _teachers = new();
    protected TeacherViewModel? _selectedTeacher;
    protected string _searchString = "";
    protected bool _isLoading = true;

    [Inject] protected NavigationManager Navigation { get; set; } = default!;
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadTeachersAsync();
    }

    private async Task LoadTeachersAsync()
    {
        _isLoading = true;

        // TODO: Replace with real service call
        await Task.Delay(500);

        _teachers = new List<TeacherViewModel>
        {
            new()
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                FullName = "Ivan Petrov",
                Email = "ivan.petrov@school.edu",
                BusinessEmail = "ivan.p@school.edu",
                BusinessPhoneNumber = "+359123456789",
                SchoolName = "Green Hill School",
                ClassName = "10A",
                Subjects = [
                    new (){
                        Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                        Name = "Maths"
                    },
                        new (){
                        Id = Guid.Parse("baaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                        Name = "Biology"
                    }
                    ]
            },
            new()
            {
                Id = Guid.NewGuid(),
                FullName = "Maria Georgieva",
                Email = "maria.georgieva@school.edu",
                BusinessEmail = "maria.g@school.edu",
                BusinessPhoneNumber = "+359987654321",
                SchoolName = "Riverdale High",
                ClassName = "11B",
                Subjects = [
                    new (){
                        Id = Guid.Parse("baaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                        Name = "Physics"
                    }
                    ]
            }
        };

        _isLoading = false;
    }

    protected void CreateTeacher()
    {
        Navigation.NavigateTo("/manage-teachers/create");
    }

    protected void EditTeacher()
    {
        if (_selectedTeacher != null)
            Navigation.NavigateTo($"/manage-teachers/edit/{_selectedTeacher.Id}");
    }

    private Func<TeacherViewModel, bool> QuickFilter => x =>
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

        if (x.SchoolName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };

}
