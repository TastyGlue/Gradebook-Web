using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login/credentials")]
    public async Task<IActionResult> LoginWithCredentials([FromBody] LoginCredentials request)
    {
        var result = await _authService.LoginWithCredentials(request);

        return ApiResponseFactory.CreateResponse<string>(result);
    }
}
