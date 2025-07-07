namespace Gradebook.Web.Components.Shared;

public partial class AbsenceDialogForm : ExtendedComponentBase
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    [Inject] protected IApiTimetableService ApiTimetableService { get; set; } = default!;
    [Parameter] public StudentViewModel Student { get; set; } = default!;
    [Parameter] public ClassViewModel Class { get; set; } = default!;
    [Parameter] public SubjectViewModel Subject { get; set; } = default!;

    protected IEnumerable<TimetableViewModel> Timetables { get; set; } = [];

    protected MudForm FormRef { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        // Fetch timetables for the current Class and Subject using ApiTimetableService

        await base.OnInitializedAsync();
    }

    protected async Task SubmitHandler()
    {
        await FormRef.Validate();

        if (FormRef.IsValid)
        {
            // Logic to add absence

            bool result = true; // Replace with actual logic to add absence

            if (result) 
            {
                Notify("Absence added successfully.", Severity.Success);
                MudDialog.Close(DialogResult.Ok(true));
            }
            else
            {
                Notify("Failed to add absence.", Severity.Error);
            }
        }
    }

    private void CancelHandler() => MudDialog.Cancel();
}
