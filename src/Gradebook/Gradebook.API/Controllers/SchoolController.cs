using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {

        private readonly ISchoolService _service;

        public SchoolController(ISchoolService service)
        {
            _service = service;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<IActionResult> GetSchools()
        {
            var result = await _service.GetAllSchoolsAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<School>, IEnumerable<SchoolDto>>(result);
        }

        // GET: api/Schools/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchool(Guid id)
        {
            var result = await _service.GetSchool(id);
            return ApiResponseFactory.AdaptAndCreateResponse<School, SchoolDto>(result);
        }

        // POST: api/Schools
        [HttpPost]
        public async Task<IActionResult> CreateSchool([FromBody] SchoolDto schoolDto)
        {
            var result = await _service.CreateSchool(schoolDto);
            return ApiResponseFactory.AdaptAndCreateResponse<School, SchoolDto>(result);
        }

        // PUT: api/Schools/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(Guid id, [FromBody] SchoolDto schoolDto)
        {
            var result = await _service.UpdateSchool(id, schoolDto);
            return ApiResponseFactory.AdaptAndCreateResponse<School, SchoolDto>(result);
        }

        // DELETE: api/Schools/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(Guid id)
        {
            var result = await _service.DeleteSchool(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }
    }
}



