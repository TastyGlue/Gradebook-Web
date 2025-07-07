namespace Gradebook.Web.Components.Pages.Teacher
{
    public partial class ClassGrades : ExtendedComponentBase
    {
        [Parameter] public Guid ClassId { get; set; }
        [Parameter] public Guid SubjectId { get; set; }

        [Inject] private IApiClassService ApiClassService { get; set; } = default!;
        [Inject] private IApiGradeService ApiGradeService { get; set; } = default!;
        [Inject] private IApiAbsencesService ApiAbsenceService { get; set; } = default!;

        private bool _isLoading = true;
        private List<StudentViewModel> _students = new();

        [Inject] private IApiStudentService ApiStudentService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            // load students by class with nested User info
            var stuRes = await ApiStudentService.GetStudentsByClassId(ClassId);
            if (!stuRes.Succeeded)
            {
                Notify(stuRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            // map DTOs (including .User) -> viewmodels
            _students = stuRes.Value!.Adapt<List<StudentViewModel>>();

            // then load grades & absences as before
            foreach (var student in _students)
            {
                var gradesRes = await ApiGradeService.GetGradesByStudentId(student.Id);
                if (gradesRes.Succeeded)
                    student.Grades = gradesRes.Value!.Select(g => g.Adapt<GradeViewModel>()).ToList();

                var absRes = await ApiAbsenceService.GetStudentAbsences(student.Id);
                if (absRes.Succeeded)
                    student.Absences = absRes.Value!.Select(a => a.Adapt<AbsenceViewModel>()).ToList();
            }

            _isLoading = false;
        }
    }
}