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

    [HttpPost("login/profile")]
    [Authorize(AuthenticationSchemes = AUTH_SCHEME)]
    public async Task<IActionResult> LoginProfile([FromBody] LoginProfile request)
    {
        var userId = HttpUtils.GetUserIdFromHttpContext(HttpContext);

        var result = await _authService.LoginProfile(request, userId!.Value);

        return ApiResponseFactory.CreateResponse<string>(result);
    }
}
