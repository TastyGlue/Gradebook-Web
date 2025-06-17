using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeachersController(ITeacherService service)
        {
            _service = service;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var result = await _service.GetAllTeachersAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<Teacher>, IEnumerable<TeacherDto>>(result);
        }

        // GET: api/Teachers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(Guid id)
        {
            var result = await _service.GetTeacher(id);
            return ApiResponseFactory.AdaptAndCreateResponse<Teacher, TeacherDto>(result);
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateUserRoleDto<TeacherDto> createUserRole)
        {
            var result = await _service.CreateTeacher(createUserRole);
            return ApiResponseFactory.AdaptAndCreateResponse<Teacher, TeacherDto>(result);
        }

        // PUT: api/Teachers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(Guid id, [FromBody] CreateUserRoleDto<TeacherDto> dto)
        {
            var result = await _service.UpdateTeacher(id, dto);
            return ApiResponseFactory.AdaptAndCreateResponse<Teacher, TeacherDto>(result);
        }

        // DELETE: api/Teachers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            var result = await _service.DeleteTeacher(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }
    }
}
