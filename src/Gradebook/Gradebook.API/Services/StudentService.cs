namespace Gradebook.API.Services
{
    public class StudentService : IStudentService
    {
        private readonly GradebookDbContext _context;
        private readonly IUserService _userService;

        public StudentService(GradebookDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<CustomResult> GetStudent(Guid id)
        {
            var student = await _context.Students
                .Include(s => s.School)
                .Include(s => s.Class)
                .Include(s => s.Grades)
                .Include(s => s.User)
                .Include(s => s.Absences)
                .Include(s => s.Parents)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return new CustomResult(new ErrorResult($"Student with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Student>(student);
        }

        public async Task<CustomResult> GetAllStudentsAsync()
        {
            var students = await _context.Students
                .Include(s => s.School)
                .Include(s => s.Class)
                .Include(s => s.Grades)
                .Include(s => s.User)
                .Include(s => s.Absences)
                .Include(s => s.Parents)
                .ThenInclude(s => s.User)
                .ToListAsync();

            return new CustomResult<IEnumerable<Student>>(students);
        }

        public async Task<CustomResult> CreateStudent(CreateUserRoleDto<StudentDto> createUserRole)
        {
            Student student = new();

            if (createUserRole.FromNewUser)
            {
                var result = await _userService.CreateUser(createUserRole.User);

                if (!result.Succeeded)
                    return result;

                var userId = (result as CustomResult<User>)!.Value!.Id;

                AdaptStudentDto(ref student, createUserRole.Role);
                student.UserId = userId;
                _context.Students.Add(student);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _context.Entry(student).State = EntityState.Detached;

                    Log.Error(Utils.GetFullExceptionMessage(ex));
                    var errorResult = ex.GetErrorMessageFromException();

                    await _userService.DeleteUser(userId);
                    await _context.SaveChangesAsync();

                    return new(errorResult);
                }
            }
            else
            {
                AdaptStudentDto(ref student, createUserRole.Role);
                _context.Students.Add(student);

                await _context.SaveChangesAsync();
            }

            return new CustomResult<Student>(student);
        }

        public async Task<CustomResult> UpdateStudent(Guid id, CreateUserRoleDto<StudentDto> createUserRole)
        {
            if (id != createUserRole.Role.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return new CustomResult(new ErrorResult("Student not found", ErrorCodes.ENTITY_NOT_FOUND));

            var result = await _userService.UpdateUser(createUserRole.Role.User!.Id, createUserRole.User);
            if (!result.Succeeded)
                return result;

            _context.Entry(student).CurrentValues.SetValues(createUserRole.Role);

            await _context.SaveChangesAsync();
            return new CustomResult<Student>(student);
        }

        public async Task<CustomResult> DeleteStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return new CustomResult(new ErrorResult("Student not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }

        public async Task<CustomResult<IEnumerable<Student>>> GetStudentsByClassIdAsync(Guid id)
        {
            var students = await _context.Students
                .Include(s => s.User)
                .Where(s => s.ClassId == id)
                .ToListAsync();

            return new CustomResult<IEnumerable<Student>>(students);
        }

        private static void AdaptStudentDto(ref Student student, StudentDto dto)
        {
            var jsonString = JsonSerializer.Serialize(dto);
            student = JsonSerializer.Deserialize<Student>(jsonString)!;

            student.School = null!;
            student.Class = null;
        }
    }
}
