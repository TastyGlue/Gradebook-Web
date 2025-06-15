using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.API.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public SubjectService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetSubject(Guid id)
        {
            var subject = await _context.Subjects
                .Include(s => s.School)
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subject == null)
                return new CustomResult(new ErrorResult($"Subject with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Subject>(subject);
        }

        public async Task<CustomResult> GetAllSubjectsAsync()
        {
            var subjects = await _context.Subjects
                .Include(s => s.School)
                .Include(s => s.Teachers)
                .ToListAsync();

            return new CustomResult<IEnumerable<Subject>>(subjects);
        }

        public async Task<CustomResult> CreateSubject(SubjectDto subjectDto)
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            subject.Id = Guid.NewGuid();

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return new CustomResult<Subject>(subject);
        }

        public async Task<CustomResult> UpdateSubject(Guid id, SubjectDto subjectDto)
        {
            if (id != subjectDto.Id)
                return new CustomResult(new ErrorResult("Mismatching IDs", ErrorCodes.ENTITY_MISMATCH_ID));

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                return new CustomResult(new ErrorResult("Subject not found", ErrorCodes.ENTITY_NOT_FOUND));

            _mapper.Map(subjectDto, subject);
            _context.Entry(subject).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new CustomResult<Subject>(subject);
        }

        public async Task<CustomResult> DeleteSubject(Guid id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                return new CustomResult(new ErrorResult("Subject not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}
