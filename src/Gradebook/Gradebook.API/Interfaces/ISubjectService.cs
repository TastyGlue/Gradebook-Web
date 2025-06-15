using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface ISubjectService
    {
        Task<CustomResult> GetSubject(Guid id);
        Task<CustomResult> GetAllSubjectsAsync();
        Task<CustomResult> CreateSubject(SubjectDto subjectDto);
        Task<CustomResult> UpdateSubject(Guid id, SubjectDto subjectDto);
        Task<CustomResult> DeleteSubject(Guid id);
    }
}
