using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface IAbsenceService
    {
        Task<CustomResult> GetAbsence(Guid id);
        Task<CustomResult> GetAllAbsencesAsync();
        Task<CustomResult> CreateAbsence(AbsenceDto absenceDto);
        Task<CustomResult> UpdateAbsence(Guid id, AbsenceDto absenceDto);
        Task<CustomResult> DeleteAbsence(Guid id);
        Task<CustomResult> GetAbsencesByStudentId(Guid id);
    }
}
