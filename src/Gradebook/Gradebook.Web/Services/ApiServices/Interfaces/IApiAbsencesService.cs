namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiAbsencesService
    {
        Task<CustomResult<IEnumerable<AbsenceDto>>> GetStudentAbsences(Guid id);
        Task<CustomResult<AbsenceDto>> CreateAbsence(AbsenceDto dto);
        Task<CustomResult<AbsenceDto>> UpdateAbsence(Guid id, AbsenceDto dto);
        Task<CustomResult<string>> DeleteAbsence(Guid id);
    }
}
