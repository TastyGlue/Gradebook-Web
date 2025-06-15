using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface ITeacherService
    {
        Task<CustomResult> GetTeacher(Guid id);
        Task<CustomResult> GetAllTeachersAsync();
        Task<CustomResult> CreateTeacher(TeacherDto teacherDto);
        Task<CustomResult> UpdateTeacher(Guid id, TeacherDto teacherDto);
        Task<CustomResult> DeleteTeacher(Guid id);
    }
}
