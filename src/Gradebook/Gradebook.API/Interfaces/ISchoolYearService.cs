using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface ISchoolYearService
    {
        Task<CustomResult> GetSchoolYear(Guid id);
        Task<CustomResult> GetAllSchoolYearsAsync();
        Task<CustomResult> CreateSchoolYear(SchoolYearDto schoolYearDto);
        Task<CustomResult> UpdateSchoolYear(Guid id, SchoolYearDto schoolYearDto);
        Task<CustomResult> DeleteSchoolYear(Guid id);
    }
}
