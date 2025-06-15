using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.API.Interfaces
{
    public interface IGradeService
    {
        Task<ActionResult<GradeDto>> GetGrade(Guid id);
        Task<IEnumerable<GradeDto>> GetAllGradesAsync();
        Task<GradeDto?> GetGradeByIdAsync(Guid id);
        Task<ActionResult<GradeDto>> CreateGrade(GradeDto gradeDto);
    }
}
