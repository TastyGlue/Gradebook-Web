using Gradebook.API.Interfaces;
using Gradebook.Data.Models;
using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.API.Services
{
    public class StudentService : IStudentService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetStudent(Guid id)
        {
            var student = await _context.Students
                .Include(s => s.School)
                .Include(s => s.Class)
                .Include(s => s.Grades)
                .Include(s => s.Absences)
                .Include(s => s.Parents)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return new CustomResult(new ErrorResult($"Student with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Student>(student);
        }

        public async Task<CustomResult> GetAllStudentsAsync()
        {
            var students = await _context.Students
                .Include(s => s.School)
                .Include(s => s.Class)
                .Include(s => s.Grades)
                .Include(s => s.Absences)
                .Include(s => s.Parents)
                .ToListAsync();

            return new CustomResult<IEnumerable<Student>>(students);
        }

        public async Task<CustomResult> CreateStudent(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            student.Id = Guid.NewGuid();

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new CustomResult<Student>(student);
        }

        public async Task<CustomResult> UpdateStudent(Guid id, StudentDto studentDto)
        {
            if (id != studentDto.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return new CustomResult(new ErrorResult("Student not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Entry(student).CurrentValues.SetValues(studentDto);

            await _context.SaveChangesAsync();
            return new CustomResult<Student>(student);
        }

        public async Task<CustomResult> DeleteStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return new CustomResult(new ErrorResult("Student not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}
