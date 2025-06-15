using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface IClassService
    {
        Task<CustomResult> GetClass(Guid id);
        Task<CustomResult> GetAllClassesAsync();
        Task<CustomResult> CreateClass(ClassDto classDto);
        Task<CustomResult> UpdateClass(Guid id, ClassDto classDto);
        Task<CustomResult> DeleteClass(Guid id);
    }
}
