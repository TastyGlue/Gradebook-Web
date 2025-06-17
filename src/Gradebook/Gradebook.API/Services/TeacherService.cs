using MapsterMapper;

namespace Gradebook.API.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public TeacherService(GradebookDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<CustomResult> GetTeacher(Guid id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.School)
                .Include(t => t.Class)
                .Include(t => t.User)
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
                return new CustomResult(new ErrorResult($"Teacher with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Teacher>(teacher);
        }

        public async Task<CustomResult> GetAllTeachersAsync()
        {
            var teachers = await _context.Teachers
                .Include(t => t.School)
                .Include(t => t.Class)
                .Include(t => t.User)
                .Include(t => t.Subjects)
                .ToListAsync();

            return new CustomResult<IEnumerable<Teacher>>(teachers);
        }

        public async Task<CustomResult> CreateTeacher(CreateUserRoleDto<TeacherDto> createUserRole)
        {
            Teacher teacher = new();

            if (createUserRole.FromNewUser)
            {
                var result = await _userService.CreateUser(createUserRole.User);

                if (!result.Succeeded)
                    return result;

                var userId = (result as CustomResult<User>)!.Value!.Id;

                AdaptTeacherDto(ref teacher, createUserRole.Role);
                _context.Teachers.Add(teacher);
                CreateTeacherSubjects(teacher, createUserRole.Role.Subjects);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Log.Error(Utils.GetFullExceptionMessage(ex));

                    var errorResult = ex.GetErrorMessageFromException();

                    await _userService.DeleteUser(userId);
                    await _context.SaveChangesAsync();

                    return new(errorResult);
                }
            }
            else
            {
                AdaptTeacherDto(ref teacher, createUserRole.Role);
                _context.Teachers.Add(teacher);
                CreateTeacherSubjects(teacher, createUserRole.Role.Subjects);

                await _context.SaveChangesAsync();
            }

            return new CustomResult<Teacher>(teacher);
        }

        public async Task<CustomResult> UpdateTeacher(Guid id, CreateUserRoleDto<TeacherDto> createUserRole)
        {
            if (id != createUserRole.Role.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var teacher = await _context.Teachers
                .Include(x => x.Subjects)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
                return new CustomResult(new ErrorResult("Teacher not found", ErrorCodes.ENTITY_NOT_FOUND));

            var result = await _userService.UpdateUser(createUserRole.User.Id, createUserRole.User);
            if (!result.Succeeded)
                return result;

            _context.Entry(teacher).CurrentValues.SetValues(createUserRole.Role);
            await EditTeacherSubjects(teacher, teacher.Subjects, createUserRole.Role.Subjects);

            await _context.SaveChangesAsync();
            return new CustomResult<Teacher>(teacher);
        }

        public async Task<CustomResult> DeleteTeacher(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
                return new CustomResult(new ErrorResult("Teacher not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }

        private static void AdaptTeacherDto(ref Teacher teacher, TeacherDto dto)
        {
            var jsonString = JsonSerializer.Serialize(dto);
            teacher = JsonSerializer.Deserialize<Teacher>(jsonString)!;

            teacher.Subjects = default!;
        }

        private void CreateTeacherSubjects(Teacher teacher, IEnumerable<SubjectDto> subjects)
        {
            if (subjects is null || !subjects.Any())
                return;
            foreach (var subject in subjects)
            {
                _context.TeacherSubjects.Add(new TeacherSubject
                {
                    TeacherId = teacher.Id,
                    SubjectId = subject.Id
                });
            }
        }

        private async Task EditTeacherSubjects(Teacher teacher, IEnumerable<Subject> oldSubjects, IEnumerable<SubjectDto> newSubjects)
        {
            var oldSubjectIds = oldSubjects.Select(x => x.Id).ToList();
            var subjectsToRemove = await _context.TeacherSubjects.Where(x => x.TeacherId == teacher.Id && oldSubjectIds.Contains(x.SubjectId)).ToListAsync();
            _context.TeacherSubjects.RemoveRange(subjectsToRemove);

            foreach (var subject in newSubjects)
            {
                _context.TeacherSubjects.Add(new()
                {
                    TeacherId = teacher.Id,
                    SubjectId = subject.Id
                });
            }
        }
    }
}
