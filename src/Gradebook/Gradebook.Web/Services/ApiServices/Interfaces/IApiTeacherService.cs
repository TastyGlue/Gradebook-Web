namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiTeacherService
    {
        Task<CustomResult<IEnumerable<TeacherDto>>> GetTeachers();
        Task<CustomResult<TeacherDto>> GetTeacher(Guid id);
        Task<CustomResult<TeacherDto>> CreateTeacher(TeacherDto dto);
        Task<CustomResult<TeacherDto>> EditTeacher(Guid id, TeacherDto dto);
    }
}
