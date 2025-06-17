namespace Gradebook.Web.Components.Pages.Administator.ManageParents
{
    public partial class Form : ExtendedComponentBase
    {
        [Parameter] public ParentViewModel ViewModel { get; set; } = new ParentViewModel();
        [Parameter] public string PageTitle { get; set; } = default!;
        [Parameter] public EventCallback OnValidSubmit { get; set; }

        protected IEnumerable<StudentViewModel> SelectedStudents { get; set; } = Array.Empty<StudentViewModel>();

        protected override void OnInitialized()
        {
            // now we’re working with the correct ViewModel
            //SelectedStudents = ViewModel.Parents ?
              //  .SelectMany(p => p.Students).ToHashSet() ?? Array.Empty<StudentViewModel>();

            if (ViewModel.Parents != null && ViewModel.Parents.Any())
                SelectedStudents = ViewModel.Parents
                    .SelectMany(p => p.Students)
                    .ToHashSet();
            else
                SelectedStudents = new HashSet<StudentViewModel>();
        }

        protected async Task ValidSubmitHandler()
            => await OnValidSubmit.InvokeAsync();

        protected void StudentClickHandler(StudentViewModel student)
            => NavigationManager.NavigateTo($"manage-students/edit/{student.Id}");

        protected void CancelHandler()
            => NavigationManager.NavigateTo("manage-parents");
    }
}
