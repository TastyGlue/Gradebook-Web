
namespace Gradebook.Web.Components.Pages.Administator.ManageSchools;

public partial class Form : ExtendedComponentBase
{
    [Parameter] public SchoolViewModelche ViewModel { get; set; } = new();
    [Parameter] public new string PageTitle { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }

    protected IEnumerable<HeadmasterViewModelche> SelectedHeadmasters { get; set; } = [];

    protected override void OnInitialized()
    {
        SelectedHeadmasters = ViewModel.Headmasters.ToHashSet();
    }

    protected async Task ValidSubmitHandler()
        => await OnValidSubmit.InvokeAsync();

    protected void HeadmasterClickHandler(HeadmasterViewModelche headmaster)
        => NavigationManager.NavigateTo($"manage-headmasters/edit/{headmaster.Id}");

    protected void CancelHandler()
        => NavigationManager.NavigateTo("manage-schools");
}
