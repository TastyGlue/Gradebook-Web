using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface ISchoolService
    {           
            Task<CustomResult> GetSchool(Guid id);
            Task<CustomResult> GetAllSchoolsAsync();
            Task<CustomResult> CreateSchool(SchoolDto schoolDto);
            Task<CustomResult> UpdateSchool(Guid id, SchoolDto schoolDto);
            Task<CustomResult> DeleteSchool(Guid id);
       
    }
}

