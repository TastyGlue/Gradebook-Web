namespace Gradebook.Web.Components.Shared;

public partial class UserForm : ComponentBase
{
    [Parameter] public UserViewModel ViewModel { get; set; } = new();

    [Parameter] public EventCallback<UserViewModel> ViewModelChanged { get; set; }
}
