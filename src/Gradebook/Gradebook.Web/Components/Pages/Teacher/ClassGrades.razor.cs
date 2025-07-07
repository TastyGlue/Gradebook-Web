namespace Gradebook.Web.Components.Pages.Teacher
{
    public partial class ClassGrades : ExtendedComponentBase
    {
        [Parameter] public Guid ClassId { get; set; }
        [Parameter] public Guid SubjectId { get; set; }

        [Inject] private IApiClassService ApiClassService { get; set; } = default!;
        [Inject] private IApiSubjectService ApiSubjectService { get; set; } = default!;
        [Inject] private IApiStudentService ApiStudentService { get; set; } = default!;
        [Inject] private IApiGradeService ApiGradeService { get; set; } = default!;
        [Inject] private IApiAbsencesService ApiAbsencesService { get; set; } = default!;

        private bool _isLoading = true;
        private string _className = "";
        private string _subjectName = "";
        private string _currentDate = DateTime.Today.ToString("dd MMM yyyy");
        private List<StudentViewModel> _students = new();

        protected override async Task OnInitializedAsync()

        {

            // 1) Load class and students
            var classRes = await ApiClassService.GetClass(ClassId);
            if (!classRes.Succeeded)
            {
                Notify(classRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            var clsVm = classRes.Value!.Adapt<ClassViewModel>();
            _className = clsVm.DisplayName;
            _students = clsVm.Students?.ToList() ?? new();

            // 2) Load subject name
            var subjRes = await ApiSubjectService.GetSubject(SubjectId);
            if (!subjRes.Succeeded)
            {
                Notify(subjRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            _subjectName = subjRes.Value!.Name;

            // 3) Load each student’s grades & absences
            foreach (var student in _students)
            {
                var gradesRes = await ApiGradeService.GetGradesByStudentId(student.Id);
                if (gradesRes.Succeeded)
                    student.Grades = gradesRes.Value!
                                        .Select(g => g.Adapt<GradeViewModel>())
                                        .ToList();

                var absRes = await ApiAbsencesService.GetStudentAbsences(student.Id);
                if (absRes.Succeeded)
                    student.Absences = absRes.Value!
                                           .Select(a => a.Adapt<AbsenceViewModel>())
                                           .ToList();
            }

            _isLoading = false;
        }

        private void ToggleAbsence(StudentViewModel student)
        {
            var existing = student.Absences.FirstOrDefault();
            if (existing == null)
            {
                // Add a new absence record
                student.Absences = new List<AbsenceViewModel>
                {
                    new AbsenceViewModel
                    {
                        Id            = Guid.Empty,
                        Date          = DateTime.Now,
                        Excused       = false,
                        IsLate        = false,
                        SchoolYearId  = Guid.Empty,        // TODO: set correct SchoolYearId
                        StudentId     = student.Id,
                        TimetableId   = Guid.Empty         // TODO: set if needed
                    }
                };
            }
            else
            {
                // Toggle Late status
                existing.IsLate = !existing.IsLate;
            }
        }

        private async Task SaveAbsences()
        {
            foreach (var student in _students)
            {
                var ab = student.Absences.FirstOrDefault();
                if (ab == null)
                    continue;

                var dto = ab.Adapt<AbsenceDto>();
                if (ab.Id == Guid.Empty)
                {
                    // Create new
                    var res = await ApiAbsencesService.CreateAbsence(dto);
                    if (!res.Succeeded)
                        Notify(res.Error!.Message, Severity.Error);
                }
                else
                {
                    // Update existing
                    var res = await ApiAbsencesService.UpdateAbsence(ab.Id, dto);
                    if (!res.Succeeded)
                        Notify(res.Error!.Message, Severity.Error);
                }
            }

            Notify("Absences saved.", Severity.Success);
        }
    }
}