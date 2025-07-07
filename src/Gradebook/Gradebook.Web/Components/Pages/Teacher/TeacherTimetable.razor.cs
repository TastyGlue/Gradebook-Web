namespace Gradebook.Web.Components.Pages.Teacher
{
    public partial class TeacherTimetable : ExtendedComponentBase
    {
        [Inject] private IApiTimetableService ApiTimetableService { get; set; } = default!;
        [Inject] private IApiTeacherService ApiTeacherService { get; set; } = default!;

        private List<TimetableViewModel> _entries = new();
        private List<TimeSpan> _times = new();
        private bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            var teacherRes = await ApiTeacherService.GetTeacher(UserStateContainer.ProfileId);
            if (!teacherRes.Succeeded)
            {
                Notify(teacherRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            var teacherId = teacherRes.Value!.Id;

            var ttRes = await ApiTimetableService.GetTimetables();
            if (!ttRes.Succeeded)
            {
                Notify(ttRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            var allEntries = ttRes.Value!.Adapt<List<TimetableViewModel>>();

            _entries = allEntries.Where(e => e.TeacherId == teacherId).ToList();
            _times = _entries.Select(e => e.TimeOfDay.TimeOfDay)
                                .Distinct()
                                .OrderBy(t => t)
                                .ToList();

            _isLoading = false;
        }
    }
}