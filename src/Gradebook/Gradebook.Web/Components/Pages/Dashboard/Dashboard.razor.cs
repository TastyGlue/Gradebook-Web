namespace Gradebook.Web.Components.Pages.Dashboard
{
    public partial class Dashboard : ExtendedComponentBase
    {
        IEnumerable<GradeDto> _grades = new List<GradeDto>();
        [Inject] IApiStudentGradeService ApiStudentGradeService { get; set; } = default!;
        [Inject] IApiAbsencesService ApiAbsencesService { get; set; } = default!;
        //Grades
        protected decimal gradeSum = 0;
        protected decimal averageGrade;
        protected string avgGradeString = default!;
        //Absences
        protected IEnumerable<AbsenceDto>? absences;
        protected int excusedAbsencesCount = 0;
        protected int inexcusedAbsencesCount = 0;
        protected int totalAbsencesCount =0;
        protected override async Task OnInitializedAsync()
        {
            await LoadGradesAsync();
            await LoadAbsencesAsync();

        }
        protected async Task LoadGradesAsync()
        {
            var result = await ApiStudentGradeService.GetStudentGrades(userStateContainer.ProfileId);
            if (result.Value != null)
                _grades = result.Value;

            if (_grades == null)
                Notify("No grades found for the student.", Severity.Warning);

            if (_grades != null && _grades.Count() > 0)
            {
                foreach (var grade in _grades)
                {
                    gradeSum += grade.Value;
                }
                averageGrade = Math.Round(gradeSum / _grades.Count(), 2);
                avgGradeString = averageGrade.ToString("F2");
            }
        }
        protected async Task LoadAbsencesAsync() 
        {
            var result = await ApiAbsencesService.GetStudentAbsences(userStateContainer.ProfileId);
            if (result != null)
                absences = result.Value;

            if (absences == null)
                Notify("The student doesn't have any absences.", Severity.Warning);

            if(absences != null && absences.Any())
            {
                excusedAbsencesCount = absences.Count(a => a.Excused);
                inexcusedAbsencesCount = absences.Count(a => !a.Excused);
                totalAbsencesCount = absences.Count();
            }
        }        
    }
}
