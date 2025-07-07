namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiParentService
    {
        Task<CustomResult<IEnumerable<ParentDto>>> GetParents();
        Task<CustomResult<ParentDto>> GetParent(Guid id);
        Task<CustomResult<ParentDto>> CreateParent(CreateUserRoleDto<ParentDto> dto);
        Task<CustomResult<ParentDto>> EditParent(Guid id, CreateUserRoleDto<ParentDto> dto);
    }
}