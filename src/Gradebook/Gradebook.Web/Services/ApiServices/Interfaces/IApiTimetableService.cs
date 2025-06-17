using Gradebook.Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gradebook.Web.Services.ApiServices.Interfaces
{
    public interface IApiTimetableService
    {
        Task<CustomResult<IEnumerable<TimetableDto>>> GetTimetables();
        Task<CustomResult<TimetableDto>> GetTimetable(Guid id);
        Task<CustomResult<TimetableDto>> CreateTimetable(TimetableDto dto);
        Task<CustomResult<TimetableDto>> EditTimetable(Guid id, TimetableDto dto);
    }
}