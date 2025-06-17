namespace Gradebook.Web.Components.Pages.Account
{
    public partial class Profile : ExtendedComponentBase
    {
        protected string FullName { get; set; } = default!;
        protected string UserEmail { get; set; } = default!;

        protected override void OnInitialized()
        {
            FullName = UserStateContainer.FullName;
            UserEmail = UserStateContainer.Email;
            base.OnInitialized();
        }

        private string Initials
        {
            get
            {
                var names = FullName.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (names.Length > 1)
                    return names[0].Substring(0, 1) + names.Last().Substring(0, 1);
                else
                    return names[0].Substring(0, 1);
            }
        }

        private DateTime? BirthDate = new DateTime(2006, 5, 15);
        private bool NotificationsEnabled = true;

        private List<DiaryEntry> DiaryEntries = new()
    {
        new DiaryEntry { Date = DateTime.Today.AddDays(-1), Content = "Finished my math homework and practiced violin." },
        new DiaryEntry { Date = DateTime.Today.AddDays(-2), Content = "Had a great science lab experiment." }
    };

        private void SaveProfile()
        {
            // Save logic here (e.g., API call)
        }

        private class DiaryEntry
        {
            public DateTime Date { get; set; }
            public string Content { get; set; }
        }

    }
}
