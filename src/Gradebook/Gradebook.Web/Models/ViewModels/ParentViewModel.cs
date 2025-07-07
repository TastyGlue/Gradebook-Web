namespace Gradebook.Web.Models.ViewModels
{
    public class ParentViewModel : ProfileViewModel
    {
        public ICollection<StudentViewModel> Students { get; set; } = [];

        public string ChildrensString => (Students.Count > 0) ? string.Join(", ", Students.Select(p => p.User?.FullName ?? "")) : string.Empty;
    }
}
