namespace Gradebook.API.Services
{
    public class ParentService : IParentService
    {
        private readonly GradebookDbContext _context;
        private readonly IUserService _userService;

        public ParentService(GradebookDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<CustomResult> GetParent(Guid id)
        {
            var parent = await _context.Parents
                .Include(p => p.User)
                .Include(p => p.Students)
                .ThenInclude(p => p.User)
                .Include(p => p.Students)
                .ThenInclude(x => x.School)
                .Include(p => p.Students)
                .ThenInclude(x => x.Class)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (parent == null)
                return new CustomResult(new ErrorResult($"Parent with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<Parent>(parent);
        }

        public async Task<CustomResult> GetAllParentsAsync()
        {
            var parents = await _context.Parents
                .Include(p => p.Students)
                .ThenInclude(p => p.User)
                .Include(p => p.User)
                .Include(p => p.Students)
                .ThenInclude(x => x.School)
                .Include(p => p.Students)
                .ThenInclude(x => x.Class)
                .ToListAsync();

            return new CustomResult<IEnumerable<Parent>>(parents);
        }

        public async Task<CustomResult> CreateParent(CreateUserRoleDto<ParentDto> createUserRole)
        {
            Parent parent = new();

            if (createUserRole.FromNewUser)
            {
                var result = await _userService.CreateUser(createUserRole.User);

                if (!result.Succeeded)
                    return result;

                var userId = (result as CustomResult<User>)!.Value!.Id;

                AdaptParentDto(ref parent, createUserRole.Role);
                parent.UserId = userId;
                _context.Parents.Add(parent);
                CreateParentStudents(parent, createUserRole.Role.Students);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _context.Entry(parent).State = EntityState.Detached;

                    Log.Error(Utils.GetFullExceptionMessage(ex));
                    var errorResult = ex.GetErrorMessageFromException();

                    await _userService.DeleteUser(userId);
                    await _context.SaveChangesAsync();

                    return new(errorResult);
                }
            }
            else
            {
                AdaptParentDto(ref parent, createUserRole.Role);
                _context.Parents.Add(parent);
                CreateParentStudents(parent, createUserRole.Role.Students);

                await _context.SaveChangesAsync();
            }

            return new CustomResult<Parent>(parent);
        }

        public async Task<CustomResult> UpdateParent(Guid id, CreateUserRoleDto<ParentDto> createUserRole)
        {
            if (id != createUserRole.Role.Id)
                return new CustomResult(new ErrorResult("Mismatching ids", ErrorCodes.ENTITY_MISMATCH_ID));

            var parent = await _context.Parents
                .Include(x => x.Students)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (parent == null)
                return new CustomResult(new ErrorResult("Parent not found", ErrorCodes.ENTITY_NOT_FOUND));

            var result = await _userService.UpdateUser(createUserRole.User.Id, createUserRole.User);
            if (!result.Succeeded)
                return result;

            _context.Entry(parent).CurrentValues.SetValues(createUserRole.Role);
            await EditParentStudents(parent, parent.Students, createUserRole.Role.Students);

            await _context.SaveChangesAsync();
            return new CustomResult<Parent>(parent);
        }

        public async Task<CustomResult> DeleteParent(Guid id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
                return new CustomResult(new ErrorResult("Parent not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }

        private static void AdaptParentDto(ref Parent parent, ParentDto dto)
        {
            foreach (var student in dto.Students)
                student.User = default!;

            var jsonString = JsonSerializer.Serialize(dto);
            parent = JsonSerializer.Deserialize<Parent>(jsonString)!;

            parent.Students = default!;
        }

        private void CreateParentStudents(Parent parent, IEnumerable<StudentDto> students)
        {
            if (students is null || !students.Any())
                return;
            foreach (var student in students)
            {
                _context.StudentParents.Add(new StudentParent
                {
                    ParentId = parent.Id,
                    StudentId = student.Id
                });
            }
        }

        private async Task EditParentStudents(Parent parent, IEnumerable<Student> oldStudents, IEnumerable<StudentDto> newStudents)
        {
            var oldStudentIds = oldStudents.Select(x => x.Id).ToList();
            var studentsToRemove = await _context.StudentParents.Where(x => x.ParentId == parent.Id && oldStudentIds.Contains(x.StudentId)).ToListAsync();
            _context.StudentParents.RemoveRange(studentsToRemove);

            foreach (var student in newStudents)
            {
                _context.StudentParents.Add(new()
                {
                    ParentId = parent.Id,
                    StudentId = student.Id
                });
            }
        }
    }
}
