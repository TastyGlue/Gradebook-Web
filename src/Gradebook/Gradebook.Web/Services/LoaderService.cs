namespace Gradebook.Web.Services;

/// <summary>
/// Provides functionality to manage and track a loading state, with support for notifying subscribers when the loading
/// state changes.
/// </summary>
/// <remarks>This service allows consumers to register a callback to be notified whenever the loading state
/// changes. The loading state can be toggled using the <see cref="ToggleLoading(bool)"/> method.</remarks>
public class LoaderService
{
    private bool _isLoading;
    private event Action<bool>? _onLoadingChanged;

    public bool IsLoading => _isLoading;

    /// <summary>
    /// Registers a callback to be invoked when the loading state changes.
    /// </summary>
    /// <param name="onLoadingChanged">A callback that receives a <see langword="true"/> value when loading starts and  <see langword="false"/> when
    /// loading ends.</param>
    public void Register(Action<bool> onLoadingChanged)
    {
        _onLoadingChanged = onLoadingChanged;
    }


    /// <summary>
    /// Toggles the loading state and notifies listeners if the state changes.
    /// </summary>
    /// <remarks>If the loading state changes, the method invokes the associated event or callback to notify
    /// listeners.</remarks>
    /// <param name="isLoading">A value indicating whether the loading state should be active.  Pass <see langword="true"/> to set the state to
    /// loading; otherwise, <see langword="false"/>.</param>
    public void ToggleLoading(bool isLoading)
    {
        if (_isLoading != isLoading)
        {
            _isLoading = isLoading;

            _onLoadingChanged?.Invoke(_isLoading);
        }
    }
}