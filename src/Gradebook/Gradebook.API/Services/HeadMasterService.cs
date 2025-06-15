using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.API.Services
{
    public class HeadmasterService : IHeadmasterService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public HeadmasterService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetHeadmaster(Guid id)
        {
            var headmaster = await _context.Headmasters
                .Include(h => h.School)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (headmaster == null)
                return new CustomResult(new ErrorResult($"Headmaster with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Headmaster>(headmaster);
        }

        public async Task<CustomResult> GetAllHeadmastersAsync()
        {
            var headmasters = await _context.Headmasters
                .Include(h => h.School)
                .ToListAsync();

            return new CustomResult<IEnumerable<Headmaster>>(headmasters);
        }

        public async Task<CustomResult> CreateHeadmaster(HeadmasterDto headmasterDto)
        {
            var headmaster = _mapper.Map<Headmaster>(headmasterDto);
            headmaster.Id = Guid.NewGuid();

            _context.Headmasters.Add(headmaster);
            await _context.SaveChangesAsync();

            return new CustomResult<Headmaster>(headmaster);
        }

        public async Task<CustomResult> UpdateHeadmaster(Guid id, HeadmasterDto headmasterDto)
        {
            if (id != headmasterDto.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var headmaster = await _context.Headmasters.FindAsync(id);
            if (headmaster == null)
                return new CustomResult(new ErrorResult("Headmaster not found", ErrorCodes.ENTITY_NOT_FOUND));

            _mapper.Map(headmasterDto, headmaster);
            _context.Entry(headmaster).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return new CustomResult<Headmaster>(headmaster);
        }

        public async Task<CustomResult> DeleteHeadmaster(Guid id)
        {
            var headmaster = await _context.Headmasters.FindAsync(id);
            if (headmaster == null)
                return new CustomResult(new ErrorResult("Headmaster not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Headmasters.Remove(headmaster);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}
