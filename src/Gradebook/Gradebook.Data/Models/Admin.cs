namespace Gradebook.Data.Models;

public class Admin : Profile, IBusinessEmail
{
    public string BusinessEmail { get; set; } = default!;
}
