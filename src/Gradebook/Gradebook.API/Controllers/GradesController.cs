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

        public GradesController(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Grades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetGrades()
        {         
            var grades = await _context.Grades
                .Include(g => g.SchoolYear)
                .Include(g => g.Subject)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .ToListAsync();

           
            return Ok(grades);
        }

        // GET: api/Grades/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeDto>> GetGrade(Guid id)
        {
            var grade = await _context.Grades
                .Include(g => g.SchoolYear)
                .Include(g => g.Subject)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grade == null)
                return NotFound();

            return Ok(grade);
        }


       

        // PUT: api/Grades/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(Guid id, GradeDto gradeDto)
        {
            if (id != gradeDto.Id)
                return BadRequest();

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
                return NotFound();

            _mapper.Map(gradeDto, grade);

            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        // DELETE: api/Grades/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(Guid id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
                return NotFound();

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradeExists(Guid id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}

