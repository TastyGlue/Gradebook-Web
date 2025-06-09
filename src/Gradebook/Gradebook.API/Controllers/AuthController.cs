using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login/credentials")]
    public async Task<IActionResult> LoginWithCredentials([FromBody] LoginCredentials request)
    {
        return Ok();
    }
}
