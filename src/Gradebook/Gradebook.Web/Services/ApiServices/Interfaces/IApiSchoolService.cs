namespace Gradebook.Web.Services.ApiServices.Interfaces;

public interface IApiSchoolService
{
    Task<CustomResult<IEnumerable<SchoolDto>>> GetSchools();

    Task<CustomResult<SchoolDto>> GetSchool(Guid id);

    Task<CustomResult<SchoolDto>> EditSchool(Guid id, SchoolDto dto);

    Task<CustomResult<SchoolDto>> CreateSchool(SchoolDto dto);
}
