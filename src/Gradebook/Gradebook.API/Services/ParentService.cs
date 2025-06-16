using Gradebook.API.Interfaces;
using Gradebook.Data.Models;
using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.API.Services
{
    public class ParentService : IParentService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public ParentService(GradebookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomResult> GetParent(Guid id)
        {
            var parent = await _context.Parents
                .Include(p => p.Students)
                .ThenInclude(p => p.User)
                .Include(p => p.User)
                .Include(p => p.Students)
                .ThenInclude(x => x.School)
                .Include(p => p.Students)
                .ThenInclude(x => x.Class)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (parent == null)
                return new CustomResult(new ErrorResult($"Parent with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Parent>(parent);
        }

        public async Task<CustomResult> GetAllParentsAsync()
        {
            var parents = await _context.Parents
                .Include(p => p.Students)
                .ThenInclude(p => p.User)
                .Include(p => p.User)
                .Include(p => p.Students)
                .ThenInclude(x => x.School)
                .Include(p => p.Students)
                .ThenInclude(x => x.Class)
                .ToListAsync();

            return new CustomResult<IEnumerable<Parent>>(parents);
        }

        public async Task<CustomResult> CreateParent(ParentDto parentDto)
        {
            var parent = _mapper.Map<Parent>(parentDto);
            parent.Id = Guid.NewGuid();

            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            return new CustomResult<Parent>(parent);
        }

        public async Task<CustomResult> UpdateParent(Guid id, ParentDto parentDto)
        {
            if (id != parentDto.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var parent = await _context.Parents
                .Include(p => p.Students)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (parent == null)
                return new CustomResult(new ErrorResult("Parent not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Entry(parent).CurrentValues.SetValues(parentDto);

            await _context.SaveChangesAsync();

            return new CustomResult<Parent>(parent);
        }

        public async Task<CustomResult> DeleteParent(Guid id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
                return new CustomResult(new ErrorResult("Parent not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}
