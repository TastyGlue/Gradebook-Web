using Microsoft.AspNetCore.Components;
using MudBlazor;
using Blazored.LocalStorage;

namespace Gradebook.Web.Components.Pages.Login
{
    public partial class LoginForm : ComponentBase
    {
        protected string _email = string.Empty;
        protected string _password = string.Empty;
        protected bool _showPassword = false;
        protected bool _rememberMe = false;
        protected bool _isLoading = false;
        protected string? _errorMessage;

        [Inject] protected ISnackbar Snackbar { get; set; } = default!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] protected ILocalStorageService LocalStorage { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            if (await LocalStorage.ContainKeyAsync("rememberedEmail"))
            {
                _email = await LocalStorage.GetItemAsync<string>("rememberedEmail");
                _rememberMe = true;
            }
        }

        protected async Task Login()
        {
            _isLoading = true;
            _errorMessage = null;

            try
            {
                await Task.Delay(1500); // Simulate API call

                if (_email == "test@example.com" && _password == "password")
                {
                    if (_rememberMe)
                        await LocalStorage.SetItemAsync("rememberedEmail", _email);
                    else
                        await LocalStorage.RemoveItemAsync("rememberedEmail");

                    Snackbar.Add("Влязохте успешно!", Severity.Success);
                    NavigationManager.NavigateTo("/dashboard");
                }
                else
                {
                    _errorMessage = "Неправилен имейл или парола.";
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Грепка при влизане: {ex.Message}";
            }
            finally
            {
                _isLoading = false;
            }
        }

        protected void TogglePasswordVisibility()
        {
            _showPassword = !_showPassword;
        }
    }
}
