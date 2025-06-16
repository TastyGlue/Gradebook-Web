namespace Gradebook.Web.Components.Pages.Administator.ManageTeachers
{
    public partial class EditTeacher : ExtendedComponentBase
    {
        [Parameter] public Guid Id { get; set; }
        [Inject] protected IApiTeacherService ApiTeacherService { get; set; } = default!;

        protected TeacherViewModel ViewModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            IsLoadingComplete = false;
            LoaderService.ToggleLoading(true);

            await GetTeacher();

            IsLoadingComplete = true;
            LoaderService.ToggleLoading(false);
        }

        private async Task GetTeacher()
        {
            var result = await ApiTeacherService.GetTeacher(Id);

            if (!result.Succeeded)
            {
                LoaderService.ToggleLoading(false);
                Notify(result.Error!.Message, Severity.Error);
                NavigationManager.NavigateTo("manage-teachers");
                return;
            }

            ViewModel = result.Value!.Adapt<TeacherViewModel>();
        }

        protected async Task ValidSubmitHandler()
        {
            var result = await ApiTeacherService.EditTeacher(Id, ViewModel.Adapt<TeacherDto>());

            if (result.Succeeded)
            {
                Notify("Operation succeeded.", Severity.Success);
                NavigationManager.NavigateTo("manage-teachers");
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
            }
        }
    }
}