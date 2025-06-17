namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiSubjectService
    {
        Task<CustomResult<IEnumerable<SubjectDto>>> GetSubjects();
        Task<CustomResult<SubjectDto>> GetSubject(Guid id);
        Task<CustomResult<SubjectDto>> CreateSubject(SubjectDto dto);
        Task<CustomResult<SubjectDto>> EditSubject(Guid id, SubjectDto dto);
    }
}
