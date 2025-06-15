using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface IParentService
    {
        Task<CustomResult> GetParent(Guid id);
        Task<CustomResult> GetAllParentsAsync();
        Task<CustomResult> CreateParent(ParentDto parentDto);
        Task<CustomResult> UpdateParent(Guid id, ParentDto parentDto);
        Task<CustomResult> DeleteParent(Guid id);
    }
}
