namespace Gradebook.Web.Components.Pages.Teacher
{
    public partial class TeacherTimetable : ExtendedComponentBase
    {
        [Inject] IApiTeacherService ApiTeacherService { get; set; } = default!;
        [Inject] IApiTimetableService ApiTimetableService { get; set; } = default!;

        private List<TimetableViewModel> _entries = new();
        private List<TimeSpan> _times = new();
        private bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            // 1. Fetch current teacher
            var teacherRes = await ApiTeacherService.GetTeacher(UserStateContainer.ProfileId);
            if (!teacherRes.Succeeded)
            {
                Notify(teacherRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            var teacherId = teacherRes.Value!.Id;

            // 2. Load all timetable entries
            var ttRes = await ApiTimetableService.GetTimetables();
            if (!ttRes.Succeeded)
            {
                Notify(ttRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            var allEntries = ttRes.Value!.Adapt<List<TimetableViewModel>>();

            // 3. Filter to current teacher
            _entries = allEntries.Where(e => e.TeacherId == teacherId).ToList();

            // 4. Determine unique times for rows
            _times = _entries
                .Select(e => e.TimeOfDay.TimeOfDay)
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            _isLoading = false;
        }
    }
}