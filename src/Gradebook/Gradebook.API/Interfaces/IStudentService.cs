using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API.Interfaces
{
    public interface IStudentService
    {
        Task<CustomResult> GetStudent(Guid id);
        Task<CustomResult> GetAllStudentsAsync();
        Task<CustomResult> CreateStudent(StudentDto studentDto);
        Task<CustomResult> UpdateStudent(Guid id, CreateUserRoleDto<StudentDto> createUserRole);
        Task<CustomResult> DeleteStudent(Guid id);
    }
}
