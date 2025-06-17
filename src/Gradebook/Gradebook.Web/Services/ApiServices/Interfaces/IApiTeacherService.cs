namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiTeacherService
    {
        Task<CustomResult<IEnumerable<TeacherDto>>> GetTeachers();
        Task<CustomResult<TeacherDto>> GetTeacher(Guid id);
        Task<CustomResult<TeacherDto>> CreateTeacher(CreateUserRoleDto<TeacherDto> dto);
        Task<CustomResult<TeacherDto>> EditTeacher(Guid id, CreateUserRoleDto<TeacherDto> dto);
    }
}
