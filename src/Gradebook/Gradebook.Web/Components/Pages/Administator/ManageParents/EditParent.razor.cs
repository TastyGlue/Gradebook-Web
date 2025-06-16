namespace Gradebook.Web.Components.Pages.Administator.ManageParents
{
    public partial class EditParent : ExtendedComponentBase
    {
        [Parameter] public Guid Id { get; set; }
        [Inject] protected IApiParentService ApiParentService { get; set; } = default!;
        protected ParentViewModel ViewModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            IsLoadingComplete = false;
            LoaderService.ToggleLoading(true);

            await LoadParentAsync();

            IsLoadingComplete = true;
            LoaderService.ToggleLoading(false);
        }

        private async Task LoadParentAsync()
        {
            var result = await ApiParentService.GetParent(Id);
            if (!result.Succeeded)
            {
                LoaderService.ToggleLoading(false);
                Notify(result.Error!.Message, Severity.Error);
                NavigationManager.NavigateTo("/manage-parents");
                return;
            }

            ViewModel = result.Value!.Adapt<ParentViewModel>();
        }

        protected async Task ValidSubmitHandler()
        {
            var dto = ViewModel.Adapt<ParentDto>();
            var result = await ApiParentService.EditParent(Id, dto);

            if (result.Succeeded)
            {
                Notify("Parent updated successfully.", Severity.Success);
                NavigationManager.NavigateTo("/manage-parents");
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
            }
        }
    }
}