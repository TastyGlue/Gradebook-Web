using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _service.GetAllUsersAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<User>, IEnumerable<UserDto>>(result);
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _service.GetUser(id);
            return ApiResponseFactory.AdaptAndCreateResponse<User, UserDto>(result);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var result = await _service.CreateUser(userDto);
            return ApiResponseFactory.AdaptAndCreateResponse<User, UserDto>(result);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto userDto)
        {
            var result = await _service.UpdateUser(id, userDto);
            return ApiResponseFactory.AdaptAndCreateResponse<User, UserDto>(result);
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _service.DeleteUser(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }

        // POST: api/Users/{id}/resetPassword
        [HttpPost("{id}/resetPassword")]
        public async Task<IActionResult> ResetPassword(Guid id)
        {
            var result = await _service.ResetPassword(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }
    }
}
