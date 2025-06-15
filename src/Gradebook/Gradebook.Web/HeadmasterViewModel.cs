namespace Gradebook.Web
{
    public class HeadmasterViewModel : IEquatable<SchoolViewModel>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BusinessEmail { get; set; } = string.Empty;
        public string BusinessPhoneNumber { get; set; } = string.Empty;
        public Guid SchoolId { get; set; }
        public bool IsActive { get; set; }

        public bool Equals(SchoolViewModel? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj) => obj is SchoolViewModel school && Equals(school);
        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString() => FullName;
    }

}
