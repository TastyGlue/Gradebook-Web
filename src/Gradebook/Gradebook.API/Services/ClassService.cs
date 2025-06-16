using Gradebook.API.Interfaces;
using Gradebook.Data.Models;
using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.API.Services
{
    public class ClassService : IClassService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public ClassService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetClass(Guid id)
        {
            var cls = await _context.Classes
                .Include(c => c.School)
                .Include(c => c.ClassTeacher)
                .Include(c => c.Students)
                .Include(c => c.Timetables)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cls == null)
                return new CustomResult(new ErrorResult($"Class with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Class>(cls);
        }

        public async Task<CustomResult> GetAllClassesAsync()
        {
            var classes = await _context.Classes
                .Include(c => c.School)
                .Include(c => c.ClassTeacher)
                .Include(c => c.Students)
                .Include(c => c.Timetables)
                .ToListAsync();

            return new CustomResult<IEnumerable<Class>>(classes);
        }

        public async Task<CustomResult> CreateClass(ClassDto classDto)
        {
            var cls = _mapper.Map<Class>(classDto);
            cls.Id = Guid.NewGuid();

            _context.Classes.Add(cls);
            await _context.SaveChangesAsync();

            return new CustomResult<Class>(cls);
        }

        public async Task<CustomResult> UpdateClass(Guid id, ClassDto classDto)
        {
            if (id != classDto.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var cls = await _context.Classes
                .Include(c => c.Students)
                .Include(c => c.Timetables)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cls == null)
                return new CustomResult(new ErrorResult("Class not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Entry(cls).CurrentValues.SetValues(classDto);

            await _context.SaveChangesAsync();

            return new CustomResult<Class>(cls);
        }

        public async Task<CustomResult> DeleteClass(Guid id)
        {
            var cls = await _context.Classes.FindAsync(id);
            if (cls == null)
                return new CustomResult(new ErrorResult("Class not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Classes.Remove(cls);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}
