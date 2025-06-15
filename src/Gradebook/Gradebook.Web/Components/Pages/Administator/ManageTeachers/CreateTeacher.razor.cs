using Microsoft.AspNetCore.SignalR;

namespace Gradebook.Web.Components.Pages.Administator.ManageTeachers
{
    public partial class CreateTeacher : ExtendedComponentBase
    {
        protected TeacherViewModel _teacher = new();
        List<SubjectViewModel> _subject = new() { new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Chemistry" } };
        private IEnumerable<string> _options = new HashSet<string>();

        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected ISnackbar Snackbar { get; set; } = default!;

        protected async Task HandleValidSubmit()
        {
            // TODO: Replace with service call
            Snackbar.Add("Teacher created successfully.", Severity.Success);
            Navigation.NavigateTo("/manage-teachers");
        }
}
}
