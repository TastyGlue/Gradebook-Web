namespace Gradebook.Web.Components.Pages.Administator.ManageSchools;

public partial class CreateSchool : ExtendedComponentBase
{
    [Inject] protected IApiSchoolService ApiSchoolService { get; set; } = default!;
    protected SchoolViewModelche ViewModel { get; set; } = new();

    protected async Task ValidSubmitHandler()
    {
        var result = await ApiSchoolService.CreateSchool(ViewModel.Adapt<SchoolDto>());

        if (result.Succeeded)
        {
            Notify("Operation succeeded.", Severity.Success);
            NavigationManager.NavigateTo("manage-schools");
        }
        else
            Notify(result.Error!.Message, Severity.Error);
    }
}
