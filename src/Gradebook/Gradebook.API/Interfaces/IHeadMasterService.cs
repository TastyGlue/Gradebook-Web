using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface IHeadmasterService
    {
        Task<CustomResult> GetHeadmaster(Guid id);
        Task<CustomResult> GetAllHeadmastersAsync();
        Task<CustomResult> CreateHeadmaster(HeadmasterDto headmasterDto);
        Task<CustomResult> UpdateHeadmaster(Guid id, HeadmasterDto headmasterDto);
        Task<CustomResult> DeleteHeadmaster(Guid id);
    }
}
