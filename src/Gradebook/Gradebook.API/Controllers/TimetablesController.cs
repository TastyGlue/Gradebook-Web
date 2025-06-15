using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimetablesController : ControllerBase
    {
        private readonly ITimetableService _service;

        public TimetablesController(ITimetableService service)
        {
            _service = service;
        }

        // GET: api/Timetables
        [HttpGet]
        public async Task<IActionResult> GetTimetables()
        {
            var result = await _service.GetAllTimetablesAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<Timetable>, IEnumerable<TimetableDto>>(result);
        }

        // GET: api/Timetables/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimetable(Guid id)
        {
            var result = await _service.GetTimetable(id);
            return ApiResponseFactory.AdaptAndCreateResponse<Timetable, TimetableDto>(result);
        }

        // POST: api/Timetables
        [HttpPost]
        public async Task<IActionResult> CreateTimetable([FromBody] TimetableDto timetableDto)
        {
            var result = await _service.CreateTimetable(timetableDto);
            return ApiResponseFactory.AdaptAndCreateResponse<Timetable, TimetableDto>(result);
        }

        // PUT: api/Timetables/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimetable(Guid id, [FromBody] TimetableDto timetableDto)
        {
            var result = await _service.UpdateTimetable(id, timetableDto);
            return ApiResponseFactory.AdaptAndCreateResponse<Timetable, TimetableDto>(result);
        }

        // DELETE: api/Timetables/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimetable(Guid id)
        {
            var result = await _service.DeleteTimetable(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }
    }
}
