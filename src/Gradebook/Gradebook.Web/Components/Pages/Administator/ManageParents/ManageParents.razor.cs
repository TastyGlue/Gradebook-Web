namespace Gradebook.Web.Components.Pages.Administator.ManageParents
{
    public partial class ManageParents : ExtendedComponentBase
    {
        [Inject] protected IApiParentService ApiParentService { get; set; } = default!;
        protected List<ParentViewModel> _parents = new();
        protected ParentViewModel? _selectedParent;
        protected string _searchString = string.Empty;
        protected bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            PageTitle = "Manage Parents";
            await LoadParentsAsync();
        }

        private async Task LoadParentsAsync()
        {
            _isLoading = true;
            var result = await ApiParentService.GetParents();
            if (result.Succeeded)
            {
                _parents = result.Value!.Adapt<List<ParentViewModel>>();
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
                NavigationManager.NavigateTo("/");
            }
            _isLoading = false;
        }

        protected void CreateParent()
            => NavigationManager.NavigateTo("/manage-parents/create");

        protected void EditParent()
        {
            if (_selectedParent != null)
                NavigationManager.NavigateTo($"/manage-parents/edit/{_selectedParent.Id}");
        }

        private bool QuickFilter(ParentViewModel p)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            return p.User.FullName.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
                || p.User.Email.Contains(_searchString, StringComparison.OrdinalIgnoreCase)
                || p.ChildrensString.Contains(_searchString, StringComparison.OrdinalIgnoreCase)               
                || (p.User.PhoneNumber?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true);
        }
    }
}