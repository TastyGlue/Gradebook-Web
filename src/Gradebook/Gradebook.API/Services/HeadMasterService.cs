namespace Gradebook.API.Services
{
    public class HeadmasterService : IHeadmasterService
    {
        private readonly GradebookDbContext _context;
        private readonly IUserService _userService;

        public HeadmasterService(GradebookDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<CustomResult> GetHeadmaster(Guid id)
        {
            var headmaster = await _context.Headmasters
                .Include(h => h.User)
                .Include(h => h.School)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (headmaster == null)
                return new CustomResult(new ErrorResult($"Headmaster with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Headmaster>(headmaster);
        }

        public async Task<CustomResult> GetAllHeadmastersAsync()
        {
            var headmasters = await _context.Headmasters
                .Include(h => h.User)
                .Include(h => h.School)
                .ToListAsync();

            return new CustomResult<IEnumerable<Headmaster>>(headmasters);
        }

        public async Task<CustomResult> CreateHeadmaster(CreateUserRoleDto<HeadmasterDto> createUserRole)
        {
            Headmaster headmaster = new();

            if (createUserRole.FromNewUser)
            {
                var result = await _userService.CreateUser(createUserRole.User);

                if (!result.Succeeded)
                    return result;

                var userId = (result as CustomResult<User>)!.Value!.Id;

                AdaptHeadmasterDto(ref headmaster, createUserRole.Role);
                headmaster.UserId = userId;
                _context.Headmasters.Add(headmaster);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _context.Entry(headmaster).State = EntityState.Detached;

                    Log.Error(Utils.GetFullExceptionMessage(ex));
                    var errorResult = ex.GetErrorMessageFromException();

                    await _userService.DeleteUser(userId);
                    await _context.SaveChangesAsync();

                    return new(errorResult);
                }
            }
            else
            {
                AdaptHeadmasterDto(ref headmaster, createUserRole.Role);

                _context.Headmasters.Add(headmaster);
                await _context.SaveChangesAsync();
            }

            return new CustomResult<Headmaster>(headmaster);
        }

        public async Task<CustomResult> UpdateHeadmaster(Guid id, CreateUserRoleDto<HeadmasterDto> createUserRole)
        {
            if (id != createUserRole.Role.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var headmaster = await _context.Headmasters.FindAsync(id);
            if (headmaster == null)
                return new CustomResult(new ErrorResult("Headmaster not found", ErrorCodes.ENTITY_NOT_FOUND));

            var result = await _userService.UpdateUser(createUserRole.Role.User!.Id, createUserRole.User);
            if (!result.Succeeded)
                return result;

            _context.Entry(headmaster).CurrentValues.SetValues(createUserRole.Role);

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

        private static void AdaptHeadmasterDto(ref Headmaster headmaster, HeadmasterDto dto)
        {
            var jsonString = JsonSerializer.Serialize(dto);
            headmaster = JsonSerializer.Deserialize<Headmaster>(jsonString)!;

            headmaster.User = null!;
            headmaster.School = null!;
        }
    }
}
