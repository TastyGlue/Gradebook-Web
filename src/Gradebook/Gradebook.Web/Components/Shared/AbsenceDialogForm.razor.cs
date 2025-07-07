namespace Gradebook.Web.Components.Shared;

public partial class AbsenceDialogForm : ExtendedComponentBase
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public TimetableViewModel Timetable { get; set; } = default!;

    [Parameter]
    public IEnumerable<StudentViewModel> Students { get; set; } = [];

    protected MudForm FormRef { get; set; } = default!;

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
