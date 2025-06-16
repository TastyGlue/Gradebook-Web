namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiStudentService
    {
        Task<CustomResult<IEnumerable<StudentDto>>> GetStudents();
        Task<CustomResult<StudentDto>> GetStudent(Guid id);
        Task<CustomResult<StudentDto>> CreateStudent(StudentDto dto);
        Task<CustomResult<StudentDto>> EditStudent(Guid id, StudentDto dto);
    }
}