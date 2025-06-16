namespace Gradebook.Web.Services.ApiServices.Interfaces;

public interface IApiHeadmasterService
{
    Task<CustomResult<IEnumerable<HeadmasterDto>>> GetHeadmasters();
}
