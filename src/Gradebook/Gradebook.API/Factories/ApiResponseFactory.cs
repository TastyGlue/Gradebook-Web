using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Factories;

public static class ApiResponseFactory
{
    /// <summary>
    /// Creates an HTTP response based on the specified <see cref="CustomResult"/>.
    /// </summary>
    /// <remarks>If the operation succeeds, the response will have a status code of 200 (OK) and include the
    /// value from the result. If the operation fails, the response will include the error details and a status code
    /// derived from the error code.</remarks>
    /// <typeparam name="T">The type of the value contained in the <see cref="CustomResult{T}"/> if the operation succeeded.</typeparam>
    /// <param name="result">The result of the operation, containing success or error information.</param>
    /// <returns>An <see cref="OkObjectResult"/> containing the value of the operation if <paramref name="result"/> indicates
    /// success; otherwise, an <see cref="ObjectResult"/> with the appropriate HTTP status code and error details.</returns>
    public static IActionResult CreateResponse<T>(CustomResult result)
    {
        if (result.Succeeded)
        {
            var value = (result as CustomResult<T>)!.Value;
            return new OkObjectResult(value);
        }
        else
        {
            var statusCode = GetHttpStatusCode(result.Error!.ErrorCode);

            return new ObjectResult(result) { StatusCode = statusCode};
        }
    }

    /// <summary>
    /// Extracts the HTTP status code from the provided error code string.
    /// </summary>
    /// <param name="errorCode">A string representing an error code, which is expected to end with an underscore followed by a three-digit HTTP
    /// status code (e.g., "ERROR_404").</param>
    /// <returns>The extracted HTTP status code as an integer. Returns <see langword="500"/> if the input is <see
    /// langword="null"/> or does not contain a valid underscore-delimited HTTP status code.</returns>
    private static int GetHttpStatusCode(string? errorCode)
    {
        if (errorCode is null)
            return 500; // Default to Internal Server Error if no error code is provided

        int lastUnderscoreIndex = errorCode.LastIndexOf('_');

        if (lastUnderscoreIndex == -1)
            return 500; // Default to Internal Server Error if underscores are not found

        return int.Parse(errorCode.Substring(lastUnderscoreIndex + 1, 3));
    }
}
