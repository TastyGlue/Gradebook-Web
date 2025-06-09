namespace Gradebook.Shared.Models.DTOs;

public class ParentDto : ProfileDto
{
    public ICollection<StudentDto> Students { get; set; } = [];
}
