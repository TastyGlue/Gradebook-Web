using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _service;

        public AbsenceController(IAbsenceService service)
        {
            _service = service;
        }

        // GET: api/Absences
        [HttpGet]
        public async Task<IActionResult> GetAbsences()
        {
            var result = await _service.GetAllAbsencesAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<Absence>, IEnumerable<AbsenceDto>>(result);
        }

        // GET: api/Absences/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAbsence(Guid id)
        {
            var result = await _service.GetAbsence(id);
            return ApiResponseFactory.AdaptAndCreateResponse<Absence, AbsenceDto>(result);
        }

        // POST: api/Absences
        [HttpPost]
        public async Task<IActionResult> CreateAbsence([FromBody] AbsenceDto absenceDto)
        {
            var result = await _service.CreateAbsence(absenceDto);
            return ApiResponseFactory.AdaptAndCreateResponse<Absence, AbsenceDto>(result);
        }

        // PUT: api/Absences/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAbsence(Guid id, [FromBody] AbsenceDto absenceDto)
        {
            var result = await _service.UpdateAbsence(id, absenceDto);
            return ApiResponseFactory.AdaptAndCreateResponse<Absence, AbsenceDto>(result);
        }

        // DELETE: api/Absences/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbsence(Guid id)
        {
            var result = await _service.DeleteAbsence(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }
        // GET: api/Absences/student{id}
        [HttpGet("student/{id}")]
        public async Task<IActionResult> GetAbsenceByStudentId(Guid id)
        {
            var result = await _service.GetAbsencesByStudentId(id);

            return ApiResponseFactory.AdaptAndCreateResponse<List<Absence>, List<AbsenceDto>>(result);
        }
    }
}
