namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiGradeService
    {
        Task<CustomResult> GetGrade(Guid id);
        Task<CustomResult<List<GradeDto>>> GetGradesByStudentId(Guid studentId);
        Task<CustomResult<IEnumerable<GradeDto>>> GetAllGradesAsync();
        Task<CustomResult<GradeDto>> CreateGrade(GradeDto dto);
        Task<CustomResult<GradeDto>> UpdateGrade(Guid id, GradeDto dto);
        Task<CustomResult<string>> DeleteGrade(Guid id);
    }
}
