using Gradebook.API.Interfaces;
using Gradebook.Data.Models;
using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.API.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public AbsenceService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetAbsence(Guid id)
        {
            var absence = await _context.Absences
                .Include(a => a.SchoolYear)
                .Include(a => a.Student)
                .Include(a => a.Timetable)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (absence == null)
                return new CustomResult(new ErrorResult($"Absence with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Absence>(absence);
        }

        public async Task<CustomResult> GetAllAbsencesAsync()
        {
            var absences = await _context.Absences
                .Include(a => a.SchoolYear)
                .Include(a => a.Student)
                .Include(a => a.Timetable)
                .ToListAsync();

            return new CustomResult<IEnumerable<Absence>>(absences);
        }

        public async Task<CustomResult> CreateAbsence(AbsenceDto absenceDto)
        {
            var absence = _mapper.Map<Absence>(absenceDto);
            absence.Id = Guid.NewGuid();

            _context.Absences.Add(absence);
            await _context.SaveChangesAsync();

            return new CustomResult<Absence>(absence);
        }

        public async Task<CustomResult> UpdateAbsence(Guid id, AbsenceDto absenceDto)
        {
            if (id != absenceDto.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var absence = await _context.Absences.FindAsync(id);
            if (absence == null)
                return new CustomResult(new ErrorResult("Absence not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Entry(absence).CurrentValues.SetValues(absenceDto);

            await _context.SaveChangesAsync();

            return new CustomResult<Absence>(absence);
        }

        public async Task<CustomResult> DeleteAbsence(Guid id)
        {
            var absence = await _context.Absences.FindAsync(id);
            if (absence == null)
                return new CustomResult(new ErrorResult("Absence not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Absences.Remove(absence);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}
