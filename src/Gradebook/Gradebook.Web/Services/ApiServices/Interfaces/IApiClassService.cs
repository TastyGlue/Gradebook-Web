namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiClassService
    {
        Task<CustomResult<IEnumerable<ClassDto>>> GetClasses();
        Task<CustomResult<ClassDto>> GetClass(Guid id);
        Task<CustomResult<ClassDto>> CreateClass(ClassDto dto);
        Task<CustomResult<ClassDto>> EditClass(Guid id, ClassDto dto);
    }
}
