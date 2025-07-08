namespace Gradebook.Web.Components.Pages.Teacher
{
    public partial class TeacherTimetable : ExtendedComponentBase
    {
        [Inject] private IApiTimetableService ApiTimetableService { get; set; } = default!;
        [Inject] private IApiTeacherService ApiTeacherService { get; set; } = default!;

        protected SchoolYearViewModel SchoolYear { get; set; } = default!;

        private List<TimetableViewModel> _entries = new();
        private List<TimeSpan> _times = new();
        private bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            if (UserStateContainer.Role != RoleType.Teacher)
            {
                _isLoading = false;
                Notify("You don't have access to this page!", Severity.Error);
                NavigationManager.NavigateTo("/dashboard");
                return;
            }

            var teacherRes = await ApiTeacherService.GetTeacher(UserStateContainer.ProfileId);
            if (!teacherRes.Succeeded)
            {
                Notify(teacherRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            var teacherId = teacherRes.Value!.Id;

            var ttRes = await ApiTimetableService.GetTimetables(UserStateContainer.ProfileId);
            if (!ttRes.Succeeded)
            {
                Notify(ttRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            var allEntries = ttRes.Value!.Adapt<List<TimetableViewModel>>();

            if (allEntries.Count > 0)
            {
                var schoolYears = allEntries.Select(e => e.SchoolYear).DistinctBy(x => x.Id).ToList();
                SchoolYear = schoolYears.OrderByDescending(sy => sy.Year).ThenByDescending(sy => sy.Semester).First();

                _entries = allEntries.Where(e => e.SchoolYearId == SchoolYear.Id).ToList();
                _times = _entries.Select(e => e.TimeOfDay.TimeOfDay)
                                    .Distinct()
                                    .OrderBy(t => t)
                                    .ToList();
            }

            _isLoading = false;
        }
    }
}