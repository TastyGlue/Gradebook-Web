namespace Gradebook.API.Interfaces;

public interface IProfileContexService
{
    Task<CustomResult<Profile>> GetProfileFromRoleType(RoleType roleType, Guid profileId, Guid userId);
}
