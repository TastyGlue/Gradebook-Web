using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectsController(ISubjectService service)
        {
            _service = service;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var result = await _service.GetAllSubjectsAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<Subject>, IEnumerable<SubjectDto>>(result);
        }

        // GET: api/Subjects/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(Guid id)
        {
            var result = await _service.GetSubject(id);
            return ApiResponseFactory.AdaptAndCreateResponse<Subject, SubjectDto>(result);
        }

        // POST: api/Subjects
        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectDto subjectDto)
        {
            var result = await _service.CreateSubject(subjectDto);
            return ApiResponseFactory.AdaptAndCreateResponse<Subject, SubjectDto>(result);
        }

        // PUT: api/Subjects/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] SubjectDto subjectDto)
        {
            var result = await _service.UpdateSubject(id, subjectDto);
            return ApiResponseFactory.AdaptAndCreateResponse<Subject, SubjectDto>(result);
        }

        // DELETE: api/Subjects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var result = await _service.DeleteSubject(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }
    }
}
