namespace Gradebook.Web.Services.ApiServices.Interfaces;

public interface IApiHeadmasterService
{
    Task<CustomResult<IEnumerable<HeadmasterDto>>> GetHeadmasters();
    Task<CustomResult<HeadmasterDto>> GetHeadmaster(Guid id);
    Task<CustomResult<HeadmasterDto>> CreateHeadmaster(CreateUserRoleDto<HeadmasterDto> dto);
    Task<CustomResult<HeadmasterDto>> EditHeadmaster(Guid id, CreateUserRoleDto<HeadmasterDto> dto);
}
