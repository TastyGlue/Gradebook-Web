namespace Gradebook.Web.Models.ViewModels
{
    public class SubjectViewModel : IEquatable<SubjectViewModel> 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid SchoolId { get; set; }

        public SchoolViewModel School { get; set; } = default!;

        public bool Equals(SubjectViewModel? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj) => obj is SubjectViewModel subject && Equals(subject);
        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString() => Name;

    }
}
