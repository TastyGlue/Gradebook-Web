using Gradebook.Shared.Models.DTOs;
using MapsterMapper;

namespace Gradebook.API.Services
{

    public class SchoolService : ISchoolService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public SchoolService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetSchool(Guid id)
        {
            var school = await _context.Schools
                .Include(s => s.Headmasters)
                .Include(s => s.SchoolYears)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (school == null)
                return new CustomResult(new ErrorResult($"School with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<School>(school);
        }

        public async Task<CustomResult> GetAllSchoolsAsync()
        {
            var schools = await _context.Schools
                .Include(s => s.Headmasters)
                .Include(s => s.SchoolYears)
                .ToListAsync();

            return new CustomResult<IEnumerable<School>>(schools);
        }

        public async Task<CustomResult> CreateSchool(SchoolDto schoolDto)
        {
            var school = _mapper.Map<School>(schoolDto);
            school.Id = Guid.NewGuid();

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return new CustomResult<School>(school);
        }

        public async Task<CustomResult> UpdateSchool(Guid id, SchoolDto schoolDto)
        {
            if (id != schoolDto.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var school = await _context.Schools
                .Include(s => s.Headmasters)
                .Include(s => s.SchoolYears)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (school == null)
                return new CustomResult(new ErrorResult("School not found", ErrorCodes.ENTITY_NOT_FOUND));

            _mapper.Map(schoolDto, school);

            _context.Entry(school).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new CustomResult<School>(school);
        }

        public async Task<CustomResult> DeleteSchool(Guid id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
                return new CustomResult(new ErrorResult("School not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}

