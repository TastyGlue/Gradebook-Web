namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiSchoolYearService
    {
        Task<CustomResult<IEnumerable<SchoolYearDto>>> GetSchoolYears();
        Task<CustomResult<SchoolYearDto>> GetSchoolYear(Guid id);
        Task<CustomResult<SchoolYearDto>> CreateSchoolYear(SchoolYearDto dto);
        Task<CustomResult<SchoolYearDto>> EditSchoolYear(Guid id, SchoolYearDto dto);
    }
}
