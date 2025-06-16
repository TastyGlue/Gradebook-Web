using Gradebook.API.Interfaces;
using Gradebook.Data.Models;
using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.API.Services
{
    public class SchoolYearService : ISchoolYearService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public SchoolYearService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetSchoolYear(Guid id)
        {
            var schoolYear = await _context.SchoolYears
                .Include(sy => sy.School)
                .Include(sy => sy.Grades)
                .Include(sy => sy.Absences)
                .Include(sy => sy.Timetables)
                .FirstOrDefaultAsync(sy => sy.Id == id);

            if (schoolYear == null)
                return new CustomResult(new ErrorResult($"SchoolYear with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<SchoolYear>(schoolYear);
        }

        public async Task<CustomResult> GetAllSchoolYearsAsync()
        {
            var schoolYears = await _context.SchoolYears
                .Include(sy => sy.School)
                .Include(sy => sy.Grades)
                .Include(sy => sy.Absences)
                .Include(sy => sy.Timetables)
                .ToListAsync();

            return new CustomResult<IEnumerable<SchoolYear>>(schoolYears);
        }

        public async Task<CustomResult> CreateSchoolYear(SchoolYearDto schoolYearDto)
        {
            var schoolYear = _mapper.Map<SchoolYear>(schoolYearDto);
            schoolYear.Id = Guid.NewGuid();

            _context.SchoolYears.Add(schoolYear);
            await _context.SaveChangesAsync();

            return new CustomResult<SchoolYear>(schoolYear);
        }

        public async Task<CustomResult> UpdateSchoolYear(Guid id, SchoolYearDto schoolYearDto)
        {
            if (id != schoolYearDto.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var schoolYear = await _context.SchoolYears.FindAsync(id);
            if (schoolYear == null)
                return new CustomResult(new ErrorResult("SchoolYear not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Entry(schoolYear).CurrentValues.SetValues(schoolYearDto);

            await _context.SaveChangesAsync();
            return new CustomResult<SchoolYear>(schoolYear);
        }

        public async Task<CustomResult> DeleteSchoolYear(Guid id)
        {
            var schoolYear = await _context.SchoolYears.FindAsync(id);
            if (schoolYear == null)
                return new CustomResult(new ErrorResult("SchoolYear not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.SchoolYears.Remove(schoolYear);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}
