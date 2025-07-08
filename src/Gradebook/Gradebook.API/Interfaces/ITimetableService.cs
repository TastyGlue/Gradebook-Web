namespace Gradebook.API.Interfaces
{
    public interface ITimetableService
    {
        Task<CustomResult> GetTimetable(Guid id);
        Task<CustomResult> GetAllTimetablesAsync(Guid? teacherId = null);
        Task<CustomResult> CreateTimetable(TimetableDto timetableDto);
        Task<CustomResult> UpdateTimetable(Guid id, TimetableDto timetableDto);
        Task<CustomResult> DeleteTimetable(Guid id);
    }
}
