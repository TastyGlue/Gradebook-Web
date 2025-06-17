using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface IGradeService
    {
        Task<CustomResult> GetGrade(Guid id);
        Task<CustomResult> GetAllGradesAsync();
        Task<CustomResult> CreateGrade(GradeDto gradeDto);
        Task<CustomResult> UpdateGrade(Guid id, GradeDto gradeDto);
        Task<CustomResult> DeleteGrade(Guid id);
        Task<CustomResult> GetGradeByStudentId(Guid id);


    }
}
