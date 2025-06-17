using Gradebook.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeadmasterController : ControllerBase
    {
        private readonly IHeadmasterService _service;

        public HeadmasterController(IHeadmasterService service)
        {
            _service = service;
        }

        // GET: api/Headmasters
        [HttpGet]
        public async Task<IActionResult> GetHeadmasters()
        {
            var result = await _service.GetAllHeadmastersAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<Headmaster>, IEnumerable<HeadmasterDto>>(result);
        }

        // GET: api/Headmasters/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHeadmaster(Guid id)
        {
            var result = await _service.GetHeadmaster(id);
            return ApiResponseFactory.AdaptAndCreateResponse<Headmaster, HeadmasterDto>(result);
        }

        // POST: api/Headmasters
        [HttpPost]
        public async Task<IActionResult> CreateHeadmaster([FromBody] CreateUserRoleDto<HeadmasterDto> createUserRole)
        {
            var result = await _service.CreateHeadmaster(createUserRole);
            return ApiResponseFactory.AdaptAndCreateResponse<Headmaster, HeadmasterDto>(result);
        }

        // PUT: api/Headmasters/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHeadmaster(Guid id, [FromBody] CreateUserRoleDto<HeadmasterDto> createUserRole)
        {
            var result = await _service.UpdateHeadmaster(id, createUserRole);
            return ApiResponseFactory.AdaptAndCreateResponse<Headmaster, HeadmasterDto>(result);
        }

        // DELETE: api/Headmasters/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeadmaster(Guid id)
        {
            var result = await _service.DeleteHeadmaster(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }
    }
}
