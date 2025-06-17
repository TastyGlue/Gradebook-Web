namespace Gradebook.Web.Components.Pages.Student
{
    public partial class Grades : ExtendedComponentBase
    {
        [Inject] protected IApiGradeService ApiGradeService { get; set; } = default!;
        [Inject] protected IApiStudentService ApiStudentService { get; set; } = default!;
        private List<GradeViewModel> _grades = new();
        private string _searchString = string.Empty;
        private bool _isLoading = true;

        protected IEnumerable<GradeViewModel> _filteredGrades =>
            string.IsNullOrWhiteSpace(_searchString)
                ? _grades
                : _grades.Where(g => g.Subject.Name
                    .Contains(_searchString, StringComparison.OrdinalIgnoreCase));

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            // 1) Get current student
            var studentRes = await ApiStudentService.GetStudent(UserStateContainer.ProfileId);
            if (!studentRes.Succeeded)
            {
                Notify(studentRes.Error!.Message, Severity.Error);
                _isLoading = false;
                return;
            }
            var student = studentRes.Value!;

            // 2) Load grades
            var gradesRes = await ApiGradeService.GetGradesByStudentId(student.Id);
            if (gradesRes.Succeeded)
            {
                // Map DTOs to ViewModels
                _grades = gradesRes.Value!
                    .Adapt<List<GradeViewModel>>();
            }
            else
            {
                Notify(gradesRes.Error!.Message, Severity.Error);
            }

            _isLoading = false;
        }

        private Func<GradeViewModel, bool> QuickFilter => grade =>
            string.IsNullOrWhiteSpace(_searchString)
                || grade.Subject.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase);
    }
}