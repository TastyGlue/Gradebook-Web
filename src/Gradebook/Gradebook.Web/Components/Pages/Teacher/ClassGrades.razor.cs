namespace Gradebook.Web.Components.Pages.Teacher
{
    public partial class ClassGrades : ExtendedComponentBase
    {
        [Parameter] public Guid ClassId { get; set; }
        [Parameter] public Guid SubjectId { get; set; }
        [Parameter] public Guid TimetableId { get; set; }
        [Parameter] public Guid SchoolYearId { get; set; }


        private bool _isLoading = true;
        private string _className = string.Empty;
        private string _subjectName = string.Empty;
        private string _currentDate = DateTime.Today.ToString("dd MMM yyyy");
        private Guid _schoolYearId;
        private List<StudentViewModel> _students = new();

        // Grade dialog state
        private bool _gradeDialogOpen;
        private StudentViewModel? _dialogStudent;
        private decimal _newGradeValue = 4.0m;

        // Absence dialog state
        private bool _absenceDialogOpen;
        private bool _newAbsenceLate;

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            // class details
            var cls = await ApiClassService.GetClass(ClassId);
            if (!cls.Succeeded) { Notify(cls.Error!.Message, Severity.Error); return; }
            _className = cls.Value!.Adapt<ClassViewModel>().DisplayName;

            // subject
            var sub = await ApiSubjectService.GetSubject(SubjectId);
            if (!sub.Succeeded) { Notify(sub.Error!.Message, Severity.Error); return; }
            _subjectName = sub.Value!.Name;

            // timetable → school year
            //var tt = await ApiTimetableService.GetTimetable(TimetableId);
            //if (tt.Succeeded) _schoolYearId = tt.Value!.SchoolYearId;

            // fetch students with nested User
            var stu = await ApiStudentService.GetStudentsByClassId(ClassId);
            if (!stu.Succeeded) { Notify(stu.Error!.Message, Severity.Error); return; }
            _students = stu.Value.Adapt<List<StudentViewModel>>();

            // load grades & absences
            foreach (var s in _students)
            {
                var gr = await ApiGradeService.GetGradesByStudentId(s.Id);
                if (gr.Succeeded)
                    s.Grades = gr.Value!.Select(g => g.Adapt<GradeViewModel>()).ToList();

                var ab = await ApiAbsencesService.GetStudentAbsences(s.Id);
                if (ab.Succeeded)
                    s.Absences = ab.Value!.Select(a => a.Adapt<AbsenceViewModel>()).ToList();
            }

            _isLoading = false;
        }

        private void OpenGradeDialog(StudentViewModel s)
        {
            _dialogStudent = s;
            _newGradeValue = 4.0m;
            _gradeDialogOpen = true;
        }

        private void CloseGradeDialog()
            => _gradeDialogOpen = false;

        private async Task SaveGrade()
        {
            if (_dialogStudent == null) return;
            var dto = new GradeDto
            {
                StudentId = _dialogStudent.Id,
                SubjectId = SubjectId,
                Value = _newGradeValue,
                Date = DateTime.Today,
                SchoolYearId = _schoolYearId
            };
            var res = await ApiGradeService.CreateGrade(dto);
            if (res.Succeeded)
            {
                _dialogStudent.Grades.Add(res.Value!.Adapt<GradeViewModel>());
                Notify("Grade added.", Severity.Success);
            }
            else
            {
                Notify(res.Error!.Message, Severity.Error);
            }
            _gradeDialogOpen = false;
        }

        private void OpenAbsenceDialog(StudentViewModel s)
        {
            _dialogStudent = s;
            _newAbsenceLate = false;
            _absenceDialogOpen = true;
        }

        private void CloseAbsenceDialog()
            => _absenceDialogOpen = false;

        private async Task SaveAbsence()
        {
            if (_dialogStudent == null) return;
            var existing = _dialogStudent.Absences.FirstOrDefault();
            AbsenceDto dto;
            if (existing == null)
            {
                dto = new AbsenceDto
                {
                    StudentId = _dialogStudent.Id,
                    Date = DateTime.Now,
                    Excused = false,
                    IsLate = _newAbsenceLate,
                    SchoolYearId = _schoolYearId,
                    TimetableId = TimetableId
                };
                var res = await ApiAbsencesService.CreateAbsence(dto);
                if (res.Succeeded)
                {
                    _dialogStudent.Absences.Add(res.Value!.Adapt<AbsenceViewModel>());
                    Notify("Absence created.", Severity.Success);
                }
                else
                    Notify(res.Error!.Message, Severity.Error);
            }
            else
            {
                existing.IsLate = _newAbsenceLate;
                dto = existing.Adapt<AbsenceDto>();
                var res = await ApiAbsencesService.UpdateAbsence(existing.Id, dto);
                if (res.Succeeded)
                    Notify("Absence updated.", Severity.Success);
                else
                    Notify(res.Error!.Message, Severity.Error);
            }
            _absenceDialogOpen = false;
        }
    }
}