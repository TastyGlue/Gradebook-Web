namespace Gradebook.Web.Components.Abstract;

public class ExtendedComponentBase : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public ProtectedLocalStorage LocalStorage { get; set; } = default!;
    [Inject] public LoaderService LoaderService { get; set; } = default!;
    [Inject] public UserStateContainer UserStateContainer { get; set; } = default!;
    [Inject] public TokenService TokenService { get; set; } = default!;
    [Inject] public ISnackbar Snackbar { get; set; } = default!;
    [Inject] public IDialogService DialogService { get; set; } = default!;
    [Inject] public IJSRuntime Js { get; set; } = default!;

    public void Notify(string message, Severity severity, int duration = 5000)
    {
        Snackbar.Add(message, severity, config => { config.VisibleStateDuration = duration; });
    }

    public async Task ConsoleLog(string message)
            => await Js.InvokeVoidAsync("console.log", message);
}
