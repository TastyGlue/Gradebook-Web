namespace Gradebook.API.Services;

public class ProfileContextService : IProfileContexService
{
    private readonly GradebookDbContext _context;

    public ProfileContextService(GradebookDbContext context)
    {
        _context = context;
    }

    public async Task<CustomResult<Profile>> GetProfileFromRoleType(RoleType roleType, Guid profileId, Guid userId)
    {
        Profile? profile = roleType switch
        {
            RoleType.Admin => await _context.Profiles
                .Include(p => p.User)
                .OfType<Admin>()
                .FirstOrDefaultAsync(p => p.Id == profileId),

            RoleType.Headmaster => await _context.Profiles
                .Include(p => p.User)
                .OfType<Headmaster>()
                .Include(p => p.School)
                .FirstOrDefaultAsync(p => p.Id == profileId),

            RoleType.Teacher => await _context.Profiles
                .Include(p => p.User)
                .OfType<Teacher>()
                .Include(p => p.School)
                .FirstOrDefaultAsync(p => p.Id == profileId),

            RoleType.Student => await _context.Profiles
                .Include(p => p.User)
                .OfType<Student>()
                .Include(p => p.School)
                .FirstOrDefaultAsync(p => p.Id == profileId),

            RoleType.Parent => await _context.Profiles
                .Include(p => p.User)
                .OfType<Parent>()
                .FirstOrDefaultAsync(p => p.Id == profileId),

            _ => throw new NotSupportedException($"Role type {roleType} not supported")
        };

        if (profile is null)
        {
            var error = new ErrorResult($"No profile found with ID: {profileId} and RoleType: {roleType}", ErrorCodes.PROFILE_NOT_FOUND);
            return new(error);
        }

        if (profile.UserId != userId)
        {
            var error = new ErrorResult($"Profile with ID: {profileId} does not belong to User with ID: {userId}", ErrorCodes.PROFILE_NOT_BELONG_TO_USER);
            return new(error);
        }

        return new(profile);
    }
}
