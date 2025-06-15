using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolYearsController : ControllerBase
    {
        private readonly ISchoolYearService _service;

        public SchoolYearsController(ISchoolYearService service)
        {
            _service = service;
        }

        // GET: api/SchoolYears
        [HttpGet]
        public async Task<IActionResult> GetSchoolYears()
        {
            var result = await _service.GetAllSchoolYearsAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<SchoolYear>, IEnumerable<SchoolYearDto>>(result);
        }

        // GET: api/SchoolYears/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolYear(Guid id)
        {
            var result = await _service.GetSchoolYear(id);
            return ApiResponseFactory.AdaptAndCreateResponse<SchoolYear, SchoolYearDto>(result);
        }

        // POST: api/SchoolYears
        [HttpPost]
        public async Task<IActionResult> CreateSchoolYear([FromBody] SchoolYearDto schoolYearDto)
        {
            var result = await _service.CreateSchoolYear(schoolYearDto);
            return ApiResponseFactory.AdaptAndCreateResponse<SchoolYear, SchoolYearDto>(result);
        }

        // PUT: api/SchoolYears/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchoolYear(Guid id, [FromBody] SchoolYearDto schoolYearDto)
        {
            var result = await _service.UpdateSchoolYear(id, schoolYearDto);
            return ApiResponseFactory.AdaptAndCreateResponse<SchoolYear, SchoolYearDto>(result);
        }

        // DELETE: api/SchoolYears/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolYear(Guid id)
        {
            var result = await _service.DeleteSchoolYear(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }
    }
}
