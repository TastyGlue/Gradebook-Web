﻿using Gradebook.API.Interfaces;
using Gradebook.API.Services;
using Gradebook.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Gradebook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var result = await _service.GetAllStudentsAsync();
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<Student>, IEnumerable<StudentDto>>(result);
        }

        // GET: api/Students/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            var result = await _service.GetStudent(id);
            return ApiResponseFactory.AdaptAndCreateResponse<Student, StudentDto>(result);
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateUserRoleDto<StudentDto> createUserRole)
        {
            var result = await _service.CreateStudent(createUserRole);
            return ApiResponseFactory.AdaptAndCreateResponse<Student, StudentDto>(result);
        }

        // PUT: api/Students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] CreateUserRoleDto<StudentDto> createUserRole)
        {
            var result = await _service.UpdateStudent(id, createUserRole);
            return ApiResponseFactory.AdaptAndCreateResponse<Student, StudentDto>(result);
        }

        // DELETE: api/Students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await _service.DeleteStudent(id);
            return ApiResponseFactory.CreateResponse<string>(result);
        }

        // GET: api/class/{id}
        [HttpGet("class/{id}")]
        public async Task<IActionResult> GetStudentsByClass(Guid id)
        {
            var result = await _service.GetStudentsByClassIdAsync(id);
            return ApiResponseFactory.AdaptAndCreateResponse<IEnumerable<Student>, IEnumerable<StudentDto>>(result);
        }
    }
}
