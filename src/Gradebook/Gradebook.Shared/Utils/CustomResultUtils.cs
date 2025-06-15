using Mapster;
using System.Text.Json;

namespace Gradebook.Shared.Utils;

public static class CustomResultUtils
{
    public static readonly JsonSerializerOptions _caseInsensitive = new() { PropertyNameCaseInsensitive = true };

    public static CustomResult<T> GetApiResponse<T>(HttpResponseMessage response, string content)
    {
        try
        {
            if (response.IsSuccessStatusCode)
            {
                if (Utils.LooksLikeJson(content))
                {
                    var result = JsonSerializer.Deserialize<T>(content, _caseInsensitive);

                    return CheckNullResult(result);
                }
                else
                {
                    // Handle conversion for simple types like int, string, etc.
                    var result = (T)Convert.ChangeType(content, typeof(T));

                    return CheckNullResult(result);
                }
            }
            else
            {
                var error = JsonSerializer.Deserialize<ErrorResult>(content, _caseInsensitive)
                    ?? new ErrorResult("Unexpected Error");

                return new(error);
            }
        }
        catch (Exception)
        {
            return new(new ErrorResult("Unexpected Error"));
        }
    }

    private static CustomResult<T> CheckNullResult<T>(T? result)
    {
        if (result is null)
        {
            var error = new ErrorResult("Unexpected Error");
            return new(error);
        }

        return new(result);
    }
}
