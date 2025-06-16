using Gradebook.API.Interfaces;
using Gradebook.Data.Models;
using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.API.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public TeacherService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetTeacher(Guid id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.School)
                .Include(t => t.Class)
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
                return new CustomResult(new ErrorResult($"Teacher with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Teacher>(teacher);
        }

        public async Task<CustomResult> GetAllTeachersAsync()
        {
            var teachers = await _context.Teachers
                .Include(t => t.School)
                .Include(t => t.Class)
                .Include(t => t.Subjects)
                .ToListAsync();

            return new CustomResult<IEnumerable<Teacher>>(teachers);
        }

        public async Task<CustomResult> CreateTeacher(TeacherDto teacherDto)
        {
            var teacher = _mapper.Map<Teacher>(teacherDto);
            teacher.Id = Guid.NewGuid();

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return new CustomResult<Teacher>(teacher);
        }

        public async Task<CustomResult> UpdateTeacher(Guid id, TeacherDto teacherDto)
        {
            if (id != teacherDto.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var teacher = await _context.Teachers
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
                return new CustomResult(new ErrorResult("Teacher not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Entry(teacher).CurrentValues.SetValues(teacherDto);

            await _context.SaveChangesAsync();
            return new CustomResult<Teacher>(teacher);
        }

        public async Task<CustomResult> DeleteTeacher(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
                return new CustomResult(new ErrorResult("Teacher not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}
