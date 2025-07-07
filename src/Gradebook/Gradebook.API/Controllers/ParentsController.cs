using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentsController : ControllerBase
    {
        private readonly IParentService _service;

        public ParentsController(IParentService service)
        {
            _service = service;
        }

        // GET: api/Parents
        [HttpGet]
        public async Task<IActionResult> GetParents()
        {
            var result = await _service.GetAllParentsAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<Parent>, IEnumerable<ParentDto>>(result);
        }

        // GET: api/Parents/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParent(Guid id)
        {
            var result = await _service.GetParent(id);
            return ApiResponseFactory.AdaptAndCreateResponse<Parent, ParentDto>(result);
        }

        // POST: api/Parents
        [HttpPost]
        public async Task<IActionResult> CreateParent([FromBody] CreateUserRoleDto<ParentDto> createUserRole)
        {
            var result = await _service.CreateParent(createUserRole);
            return ApiResponseFactory.AdaptAndCreateResponse<Parent, ParentDto>(result);
        }

        // PUT: api/Parents/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParent(Guid id, [FromBody] CreateUserRoleDto<ParentDto> createUserRole)
        {
            var result = await _service.UpdateParent(id, createUserRole);
            return ApiResponseFactory.AdaptAndCreateResponse<Parent, ParentDto>(result);
        }

        // DELETE: api/Parents/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParent(Guid id)
        {
            var result = await _service.DeleteParent(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }
    }
}
