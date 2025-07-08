namespace Gradebook.Web.Components.Pages.Teacher
{
    public partial class TimetableEntry : ExtendedComponentBase
    {
        [Inject] protected IApiTimetableService ApiTimetableService { get; set; } = default!;
        [Parameter] public Guid TimetableId { get; set; }

        protected TimetableViewModel Timetable { get; set; } = default!;

        protected ClassViewModel Class { get; set; } = new();
        protected SubjectViewModel Subject { get; set; } = new();

        protected DialogOptions FormDialogOptions = new() { CloseButton = true, MaxWidth = MaxWidth.Medium, BackdropClick = true, CloseOnEscapeKey = true, Position = DialogPosition.Center };


        private bool _isLoading = true;
        private string _className = string.Empty;
        private string _subjectName = string.Empty;
        private string _currentDate = DateTime.Today.ToString("dd MMM yyyy");
        private string _dayAndTime = string.Empty;
        private Guid _schoolYearId;
        private List<StudentViewModel> _students = new();

        // Grade dialog state
        private bool _gradeDialogOpen;
        private StudentViewModel? _dialogStudent;
        private decimal _newGradeValue = 4.0m;

        // Absence dialog state
        private bool _absenceDialogOpen;
        private bool _newAbsenceLate;
        private DateTime? _newAbsenceDate = DateTime.Now;
        private MudForm AbsenceForm = default!;

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            var timetableRes = await ApiTimetableService.GetTimetable(TimetableId);
            if (!timetableRes.Succeeded)
            {
                Notify(timetableRes.Error!.Message, Severity.Error);
                _isLoading = false;
                NavigationManager.NavigateTo("/teacher-timetable");
                return;
            }

            Timetable = timetableRes.Value!.Adapt<TimetableViewModel>();

            if (Timetable.Class is null)
            {
                Notify("Timetable entry is inaccessible since it doesn't have a Class", Severity.Warning);
                _isLoading = false;
                NavigationManager.NavigateTo("/teacher-timetable");
                return;
            }

            _dayAndTime = $"{Timetable.DayOfWeek.ToString()} - {Timetable.TimeOfDay.ToString("HH:mm")}";

            // class details
            Class = Timetable.Class;
            _className = Class.DisplayName;

            // subject
            Subject = Timetable.Subject;
            _subjectName = Subject.Name;

            // students
            _students = Timetable.Class.Students.ToList();

            // load grades
            foreach (var s in _students)
            {
                var gr = await ApiGradeService.GetGradesByStudentId(s.Id);
                if (gr.Succeeded)
                    s.Grades = gr.Value!.Select(g => g.Adapt<GradeViewModel>()).ToList();
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
                SubjectId = Timetable.SubjectId,
                Value = _newGradeValue,
                Date = DateTime.Today,
                SchoolYearId = _schoolYearId
            };
            var res = await ApiGradeService.CreateGrade(dto);
            if (res.Succeeded)
            {
                _dialogStudent.Grades.Add(res.Value!.Adapt<GradeViewModel>());
                Notify("Grade added successfully.", Severity.Success);
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

            var dto = new AbsenceDto
            {
                StudentId = _dialogStudent.Id,
                Date = _newAbsenceDate!.Value,
                Excused = false,
                IsLate = _newAbsenceLate,
                SchoolYearId = Timetable.SchoolYearId,
                TimetableId = TimetableId
            };
            var res = await ApiAbsencesService.CreateAbsence(dto);
            if (res.Succeeded)
            {
                _dialogStudent.Absences.Add(res.Value!.Adapt<AbsenceViewModel>());
                Notify("Absence created successfully.", Severity.Success);
            }
            else
                Notify(res.Error!.Message, Severity.Error);

            //var existing = _dialogStudent.Absences.FirstOrDefault();
            //AbsenceDto dto;
            //if (existing == null)
            //{
            //    dto = new AbsenceDto
            //    {
            //        StudentId = _dialogStudent.Id,
            //        Date = _newAbsenceDate!.Value,
            //        Excused = false,
            //        IsLate = _newAbsenceLate,
            //        SchoolYearId = Timetable.SchoolYearId,
            //        TimetableId = TimetableId
            //    };
            //    var res = await ApiAbsencesService.CreateAbsence(dto);
            //    if (res.Succeeded)
            //    {
            //        _dialogStudent.Absences.Add(res.Value!.Adapt<AbsenceViewModel>());
            //        Notify("Absence created.", Severity.Success);
            //    }
            //    else
            //        Notify(res.Error!.Message, Severity.Error);
            //}
            //else
            //{
            //    existing.IsLate = _newAbsenceLate;
            //    dto = existing.Adapt<AbsenceDto>();
            //    var res = await ApiAbsencesService.UpdateAbsence(existing.Id, dto);
            //    if (res.Succeeded)
            //        Notify("Absence updated.", Severity.Success);
            //    else
            //        Notify(res.Error!.Message, Severity.Error);
            //}

            _absenceDialogOpen = false;
        }

        private IEnumerable<string> AbsenceDateValidity(DateTime? value)
        {
            if (value is null || !value.HasValue)
                yield return "Date is required.";
            else if (value.Value > DateTime.Now)
                yield return "Date cannot be in the future.";
            else if (Timetable.SchoolYear.Start.HasValue && value.Value < Timetable.SchoolYear.Start)
                yield return $"Date must be within the school year ({Timetable.SchoolYear.Start.Value.ToString("dd.MM.yyyy")}.";
            else if (value.Value.DayOfWeek != Timetable.DayOfWeek)
                yield return $"Date must be a {Timetable.DayOfWeek}.";
        }
    }
}