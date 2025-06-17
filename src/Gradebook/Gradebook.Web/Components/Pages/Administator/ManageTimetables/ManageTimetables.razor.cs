namespace Gradebook.Web.Components.Pages.Administator.ManageTimetables
{
    public partial class ManageTimetables : ExtendedComponentBase
    {
        [Inject] protected IApiTimetableService ApiTimetableService { get; set; } = default!;

        protected List<TimetableViewModel> _timetables = new();
        protected TimetableViewModel? _selectedTimetable;
        protected string _searchString = string.Empty;
        protected bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            PageTitle = "Manage Timetables";
            await LoadTimetablesAsync();
        }

        private async Task LoadTimetablesAsync()
        {
            _isLoading = true;
            var result = await ApiTimetableService.GetTimetables();
            if (result.Succeeded)
            {
                _timetables = result.Value!.Adapt<List<TimetableViewModel>>();
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
                NavigationManager.NavigateTo("/");
            }
            _isLoading = false;
        }

        protected void CreateTimetable()
            => NavigationManager.NavigateTo("/manage-timetables/create");

        protected void EditTimetable()
        {
            if (_selectedTimetable != null)
                NavigationManager.NavigateTo($"/manage-timetables/edit/{_selectedTimetable.Id}");
        }

        private bool QuickFilter(TimetableViewModel x)
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            var s = _searchString.Trim();
            if (x.SchoolYear.School.Name
                    .Contains(s, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.SchoolYear.Year.ToString()
                    .Contains(s, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.SchoolYear.Semester.ToString()
                    .Contains(s, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.DayOfWeek.ToString()
                    .Contains(s, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.TimeOfDay.ToString("t")
                    .Contains(s, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.Teacher?.User.FullName
                    .Contains(s, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            if (x.Class?.DisplayName
                    .Contains(s, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            if (x.Subject.Name
                    .Contains(s, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }
}