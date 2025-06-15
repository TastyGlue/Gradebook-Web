using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _service;

        public ClassesController(IClassService service)
        {
            _service = service;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<IActionResult> GetClasses()
        {
            var result = await _service.GetAllClassesAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<Class>, IEnumerable<ClassDto>>(result);
        }

        // GET: api/Classes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass(Guid id)
        {
            var result = await _service.GetClass(id);
            return ApiResponseFactory.AdaptAndCreateResponse<Class, ClassDto>(result);
        }

        // POST: api/Classes
        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] ClassDto classDto)
        {
            var result = await _service.CreateClass(classDto);
            return ApiResponseFactory.AdaptAndCreateResponse<Class, ClassDto>(result);
        }

        // PUT: api/Classes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(Guid id, [FromBody] ClassDto classDto)
        {
            var result = await _service.UpdateClass(id, classDto);
            return ApiResponseFactory.AdaptAndCreateResponse<Class, ClassDto>(result);
        }

        // DELETE: api/Classes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(Guid id)
        {
            var result = await _service.DeleteClass(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }
    }
}
