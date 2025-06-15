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
                var result = JsonSerializer.Deserialize<T>(content, _caseInsensitive);
                //var result = content.Adapt<T>();

                if (result is null)
                {
                    var error = new ErrorResult("Unexpected Error");
                    return new(error);
                }

                return new(result);
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
}
