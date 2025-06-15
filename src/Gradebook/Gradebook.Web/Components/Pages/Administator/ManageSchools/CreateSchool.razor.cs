namespace Gradebook.Web.Components.Pages.Administator.ManageSchools
{
    public partial class CreateSchoolBase : ComponentBase
    {
        protected SchoolViewModel School { get; set; } = new();

        protected MudForm _form = default!;

        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected ISnackbar Snackbar { get; set; } = default!;

        protected async Task SubmitAsync()
        {
            await _form.Validate();

            if (_form.IsValid)
            {
                // TODO: Save logic (via service)
                Snackbar.Add("School created successfully!", Severity.Success);
                Navigation.NavigateTo("/manage-schools");
            }
            else
            {
                Snackbar.Add("Please fill all required fields.", Severity.Warning);
            }
        }

        protected void GoBack()
        {
            Navigation.NavigateTo("/manage-schools");
        }
    }
}
