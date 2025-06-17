namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiAbsencesService
    {
        Task<CustomResult<IEnumerable<AbsenceDto>>> GetStudentAbsences(Guid id);
    }
}
