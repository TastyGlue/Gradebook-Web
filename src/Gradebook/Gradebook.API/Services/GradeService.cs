using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gradebook.Data.Models;

namespace Gradebook.API.Services
{
    public class GradeService : IGradeService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;
        public GradeService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CustomResult> GetGrade(Guid id)
        {
            var grade = await _context.Grades
                .Include(g => g.SchoolYear)
                .Include(g => g.Subject)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grade == null)
                return new (new ErrorResult($"Grade with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Grade>(grade);
        }
        public async Task<CustomResult> GetGradeByStudentId(Guid id)
        {
            var grades = await _context.Grades
                .Include(g => g.SchoolYear)
                .Include(g => g.Subject)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .Where(g => g.StudentId == id)
                .ToListAsync();

            if (grades == null)
                return new(new ErrorResult($"Grade with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<List<Grade>>(grades);
        }
        public async Task<CustomResult> GetAllGradesAsync()
        {
            var grades = await _context.Grades
                .Include(g => g.SchoolYear)
                .Include(g => g.Subject)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .ToListAsync();

            return new CustomResult<IEnumerable<Grade>>(grades);
        }
        public async Task<CustomResult> CreateGrade(GradeDto gradeDto)
        {
            var grade = _mapper.Map<Grade>(gradeDto);
            grade.Id = Guid.NewGuid();

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return new CustomResult<Grade>(grade);
        }
        public async Task<CustomResult> UpdateGrade(Guid id, GradeDto gradeDto)
        {
            if (id != gradeDto.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
                return new CustomResult(new ErrorResult("Grade not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Entry(grade).CurrentValues.SetValues(gradeDto);

            await _context.SaveChangesAsync();
           
            return new CustomResult<Grade>(grade);
        }
        public async Task<CustomResult> DeleteGrade(Guid id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
                return new CustomResult(new ErrorResult("Grade not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }

    }
}
