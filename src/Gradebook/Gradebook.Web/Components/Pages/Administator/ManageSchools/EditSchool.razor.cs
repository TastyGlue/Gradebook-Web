using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace Gradebook.Web.Components.Pages.Administator.ManageSchools
{
    public partial class EditSchool : ExtendedComponentBase
    {
        [Parameter] public Guid Id { get; set; }
        protected SchoolViewModel Model { get; set; } = new();

        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadSchoolAsync();
        }

        private async Task LoadSchoolAsync()
        {
            // TODO: Replace with real data fetching logic
            await Task.Delay(300);
            var school = GetMockSchools().FirstOrDefault(s => s.Id == Id);

            if (school != null)
            {
                Model = new SchoolViewModel
                {
                    Id = school.Id,
                    Name = school.Name,
                    Address = school.Address,
                    Website = school.Website,
                    Headmasters = school.Headmasters
                };
            }
            else
            {
                Snackbar.Add("School not found.", Severity.Error);
                Navigation.NavigateTo("/manage-schools");
            }
        }

        protected async Task HandleValidSubmit()
        {
            // TODO: Save the changes via service
            await Task.Delay(300); // Simulate save

            Snackbar.Add("School updated successfully!", Severity.Success);
            Navigation.NavigateTo("/manage-schools");
        }

        // For test, to be deleted
        private List<SchoolViewModel> GetMockSchools() => new()
        {
            new() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Green Hill School", Address = "123 Green St", Website = "https://greenhill.edu", Headmasters = "Valentin Stonev" },
            new() { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Riverdale High", Address = "456 River Rd", Website = "https://riverdale.edu", Headmasters = "Joseph Santer" }
        };
    }
}
