using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface ITeacherService
    {
        Task<CustomResult> GetTeacher(Guid id);
        Task<CustomResult> GetAllTeachersAsync();
        Task<CustomResult> CreateTeacher(CreateUserRoleDto<TeacherDto> createUserRole);
        Task<CustomResult> UpdateTeacher(Guid id, CreateUserRoleDto<TeacherDto> dto);
        Task<CustomResult> DeleteTeacher(Guid id);
    }
}
