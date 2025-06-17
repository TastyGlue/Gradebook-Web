namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiStudentGradeService
    {
        Task<CustomResult<IEnumerable<GradeDto>>>GetStudentGrades(Guid id);
    }
}
