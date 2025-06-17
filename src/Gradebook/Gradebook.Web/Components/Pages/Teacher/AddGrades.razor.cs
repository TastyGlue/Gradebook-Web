namespace Gradebook.Web.Components.Pages.Teacher.AddGrades
{
    public partial class AddGrades : ExtendedComponentBase
    {
        [Inject] protected IApiTeacherService ApiTeacherService { get; set; } = default!;
        [Inject] protected IApiClassService ApiClassService { get; set; } = default!;
        [Inject] protected IApiGradeService ApiGradeService { get; set; } = default!;

        private MudForm _form = default!;
        protected GradeCreateModel Model { get; set; } = new();
        protected List<ClassViewModel> Classes { get; set; } = new();
        protected List<StudentViewModel> Students { get; set; } = new();
        protected List<SubjectViewModel> Subjects { get; set; } = new();
        protected bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            var teacherRes = await ApiTeacherService.GetTeacher(UserStateContainer.ProfileId);
            if (!teacherRes.Succeeded)
            {
                Notify(teacherRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            var dto = teacherRes.Value!;

            // Map navigations
            if (dto.Class != null)
                Classes = new List<ClassViewModel> { dto.Class.Adapt<ClassViewModel>() };

            Subjects = dto.Subjects?.Adapt<List<SubjectViewModel>>() ?? new();

            _isLoading = false;
        }

        protected void OnClassChanged(Guid classId)
        {
            Model.ClassId = classId;
            var cls = Classes.FirstOrDefault(c => c.Id == classId);
            Students = cls?.Students?.ToList() ?? new();
        }

        protected async Task OnSubmitClicked()
        {
            await _form.Validate();
            if (!_form.IsValid)
                return;

            var dto = new GradeDto
            {
                StudentId = Model.StudentId,
                SubjectId = Model.SubjectId,
                Value = Model.Value,
                Date = Model.Date,
                SchoolYearId = Model.SchoolYearId
            };

            var res = await ApiGradeService.CreateGrade(dto);
            if (res.Succeeded)
            {
                Notify("Grade added successfully.", Severity.Success);
                _form.ResetValidation();
                Model = new GradeCreateModel();
            }
            else
                Notify(res.Error!.Message, Severity.Error);
        }

        public class GradeCreateModel
        {
            public Guid ClassId { get; set; }
            public Guid StudentId { get; set; }
            public Guid SubjectId { get; set; }
            public decimal Value { get; set; }
            public DateTime Date { get; set; } = DateTime.Today;
            public Guid SchoolYearId { get; set; }
        }
    }
}