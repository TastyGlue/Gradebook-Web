using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface IParentService
    {
        Task<CustomResult> GetParent(Guid id);
        Task<CustomResult> GetAllParentsAsync();
        Task<CustomResult> CreateParent(CreateUserRoleDto<ParentDto> createUserRole);
        Task<CustomResult> UpdateParent(Guid id, CreateUserRoleDto<ParentDto> createUserRole);
        Task<CustomResult> DeleteParent(Guid id);
    }
}
