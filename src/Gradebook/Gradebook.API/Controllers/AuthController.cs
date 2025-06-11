using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(IAuthService authService, IOptions<JwtSettings> jwtSettings)
    {
        _authService = authService;
        _jwtSettings = jwtSettings.Value;
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
        var userId = HttpUtils.GetUserIdFrom(HttpContext);

        var result = await _authService.LoginProfile(request, userId!.Value);

        return ApiResponseFactory.CreateResponse<string>(result);
    }

    [HttpPost("login/profile/token")]
    [Authorize(AuthenticationSchemes = PROFILE_TOKEN_SCHEME)]
    public async Task<IActionResult> LoginWithProfileToken([FromBody] LoginProfile request)
    {
        var expirationTimespan = TimeSpan.FromDays(_jwtSettings.RefreshTokenExpirationDays);

        var isTokenExpired = HttpUtils.IsTokenExpired(HttpContext, expirationTimespan);

        if (isTokenExpired is null)
        {
            var errorResult = new CustomResult(new ErrorResult("Token is not valid", ErrorCodes.LOGIN_PROFILE_TOKEN_INVALID));
            return ApiResponseFactory.CreateResponse<object>(errorResult);
        }

        if (isTokenExpired.Value)
        {
            var errorResult = new CustomResult(new ErrorResult("Token is expired", ErrorCodes.LOGIN_PROFILE_TOKEN_EXPIRED));
            return ApiResponseFactory.CreateResponse<object>(errorResult);
        }

        var userId = HttpUtils.GetUserIdFrom(HttpContext);

        var result = await _authService.LoginProfile(request, userId!.Value);

        return ApiResponseFactory.CreateResponse<string>(result);
    }
}
