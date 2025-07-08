using MapsterMapper;

namespace Gradebook.API.Services
{
    public class TimetableService : ITimetableService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public TimetableService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetTimetable(Guid id)
        {
            var timetable = await _context.Timetables
                .Include(t => t.SchoolYear)
                .ThenInclude(t => t.School)
                .Include(t => t.Subject)
                .Include(t => t.Class)
                    .ThenInclude(t => t.Students)
                        .ThenInclude(t => t.Absences)
                .Include(t => t.Class)
                    .ThenInclude(t => t.Students)
                        .ThenInclude(t => t.User)
                .Include(t => t.Teacher)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (timetable == null)
                return new CustomResult(new ErrorResult($"Timetable with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Timetable>(timetable);
        }

        public async Task<CustomResult> GetAllTimetablesAsync(Guid? teacherId = null)
        {
            var timetablesQuery = _context.Timetables
                .Include(t => t.SchoolYear)
                .ThenInclude(t => t.School)
                .Include(t => t.Subject)
                .Include(t => t.Class)
                .Include(t => t.Teacher)
                .ThenInclude(t => t.User)
                .AsNoTracking();

            if (teacherId.HasValue)
                timetablesQuery = timetablesQuery.Where(t => t.TeacherId == teacherId.Value);

            var timetables = await timetablesQuery.ToListAsync();

            return new CustomResult<IEnumerable<Timetable>>(timetables);
        }

        public async Task<CustomResult> CreateTimetable(TimetableDto timetableDto)
        {
            var timetable = _mapper.Map<Timetable>(timetableDto);
            timetable.Id = Guid.NewGuid();

            timetable.TimeOfDay = DateTime.SpecifyKind(timetable.TimeOfDay, DateTimeKind.Utc);
            _context.Timetables.Add(timetable);
            await _context.SaveChangesAsync();

            return new CustomResult<Timetable>(timetable);
        }

        public async Task<CustomResult> UpdateTimetable(Guid id, TimetableDto timetableDto)
        {
            if (id != timetableDto.Id)
                return new CustomResult(new ErrorResult("Mismatching IDs", ErrorCodes.ENTITY_MISMATCH_ID));

            var timetable = await _context.Timetables.FindAsync(id);
            if (timetable == null)
                return new CustomResult(new ErrorResult("Timetable not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Entry(timetable).CurrentValues.SetValues(timetableDto);

            await _context.SaveChangesAsync();

            return new CustomResult<Timetable>(timetable);
        }

        public async Task<CustomResult> DeleteTimetable(Guid id)
        {
            var timetable = await _context.Timetables.FindAsync(id);
            if (timetable == null)
                return new CustomResult(new ErrorResult("Timetable not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Timetables.Remove(timetable);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}
