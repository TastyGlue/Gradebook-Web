namespace Gradebook.Web.Components.Pages.Administator.ManageTimetables
{
    public partial class CreateTimetable : ExtendedComponentBase
    {
        [Inject] protected IApiTimetableService ApiTimetableService { get; set; } = default!;
        [Inject] protected IApiSchoolYearService ApiSchoolYearService { get; set; } = default!;
        [Inject] protected IApiTeacherService ApiTeacherService { get; set; } = default!;
        [Inject] protected IApiClassService ApiClassService { get; set; } = default!;
        [Inject] protected IApiSubjectService ApiSubjectService { get; set; } = default!;
        private MudForm _form = default!;
        protected TimetableViewModel ViewModel { get; set; } = new();
        protected List<SchoolYearViewModel> SchoolYears { get; set; } = new();
        protected List<TeacherViewModel> Teachers { get; set; } = new();
        protected List<ClassViewModel> Classes { get; set; } = new();
        protected List<SubjectViewModel> Subjects { get; set; } = new();

        protected TimeSpan? SelectedTime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadDropdownsAsync();
        }

        private async Task LoadDropdownsAsync()
        {
            var syRes = await ApiSchoolYearService.GetSchoolYears();
            if (syRes.Succeeded)
                SchoolYears = syRes.Value!.Adapt<List<SchoolYearViewModel>>();
            else
                Notify(syRes.Error!.Message, Severity.Error);

            var tRes = await ApiTeacherService.GetTeachers();
            if (tRes.Succeeded)
                Teachers = tRes.Value!.Adapt<List<TeacherViewModel>>();
            else
                Notify(tRes.Error!.Message, Severity.Error);

            var cRes = await ApiClassService.GetClasses();
            if (cRes.Succeeded)
                Classes = cRes.Value!.Adapt<List<ClassViewModel>>();
            else
                Notify(cRes.Error!.Message, Severity.Error);

            var sRes = await ApiSubjectService.GetSubjects();
            if (sRes.Succeeded)
                Subjects = sRes.Value!.Adapt<List<SubjectViewModel>>();
            else
                Notify(sRes.Error!.Message, Severity.Error);
        }

        protected void SelectedTimeChanged(TimeSpan? value)
        {
            SelectedTime = value;
            ViewModel.TimeOfDay = DateTime.Today.Add(value ?? TimeSpan.Zero);
        }

        protected async Task ValidSubmitHandler()
        {
            await _form.Validate();
            if (!_form.IsValid)
                return;

            var dto = ViewModel.Adapt<TimetableDto>();
            var result = await ApiTimetableService.CreateTimetable(dto);

            if (result.Succeeded)
            {
                Notify("Timetable entry created.", Severity.Success);
                NavigationManager.NavigateTo("/manage-timetables");
            }
            else
            {
                Notify(result.Error!.Message, Severity.Error);
            }
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/manage-timetables");
        }
    }
}