using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gradebook.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGradeService _service;

        public GradesController(GradebookDbContext context, IMapper mapper, IGradeService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        // GET: api/Grades
        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {         
            var result = await _service.GetAllGradesAsync();
            
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<Grade>, IEnumerable<GradeDto>>(result);
        }
        // GET: api/Grades/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrade(Guid id)
        {
            var result = await _service.GetGrade(id);

            return ApiResponseFactory.AdaptAndCreateResponse<Grade, GradeDto>(result);
        }   
        // PUT: api/Grades/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(Guid id, [FromBody] GradeDto gradeDto)
        {
            var result = await _service.UpdateGrade(id, gradeDto);

            return ApiResponseFactory.AdaptAndCreateResponse<Grade, GradeDto>(result);
        }


        // DELETE: api/Grades/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(Guid id)
        {
            var result = await _service.DeleteGrade(id);

            return ApiResponseFactory.CreateResponse<string>(result);
        }


    }
}

