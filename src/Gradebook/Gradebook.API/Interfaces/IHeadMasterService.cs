namespace Gradebook.API.Interfaces
{
    public interface IHeadmasterService
    {
        Task<CustomResult> GetHeadmaster(Guid id);
        Task<CustomResult> GetAllHeadmastersAsync();
        Task<CustomResult> CreateHeadmaster(CreateUserRoleDto<HeadmasterDto> createUserRole);
        Task<CustomResult> UpdateHeadmaster(Guid id, CreateUserRoleDto<HeadmasterDto> createUserRole);
        Task<CustomResult> DeleteHeadmaster(Guid id);
    }
}
