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

            return new CustomResult<GradeDto>(grade.Adapt<GradeDto>());
        }
        public async Task<CustomResult<IEnumerable<GradeDto>>> GetAllGradesAsync()
        {
            var grades = await _context.Grades
                .Include(g => g.SchoolYear)
                .Include(g => g.Subject)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .ToListAsync();
            return _mapper.Map<IEnumerable<GradeDto>>(grades);
        }
        public async Task<CustomResult> GetGradeByIdAsync(Guid id)
        {
            var grade = await _context.Grades
                .Include(g => g.SchoolYear)
                .Include(g => g.Subject)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(g => g.Id == id);
            return grade is null ? null : _mapper.Map<GradeDto>(grade);
        }
        public async Task<CustomResult<Grade>> CreateGrade(GradeDto gradeDto)
        {
            var grade = _mapper.Map<Grade>(gradeDto);
            grade.Id = Guid.NewGuid();

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return new CustomResult<Grade>(grade);
        }
    }
}
