namespace Gradebook.Web.Services;

public class HttpClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly NavigationManager _navigationManager;

    public HttpClientService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
    {
        _httpClientFactory = httpClientFactory;
        _navigationManager = navigationManager;
    }

    public HttpClient CreateApiClient(string? accessToken = null)
    {
        var client = _httpClientFactory.CreateClient(Constants.API_CLIENT_NAME);

        if (client is null)
            _navigationManager.NavigateTo("/error");

        if (accessToken is not null)
            client!.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

        return client!;
    }
}