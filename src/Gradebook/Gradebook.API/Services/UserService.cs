using Gradebook.API.Interfaces;
using Gradebook.Shared.Models.DTOs;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly GradebookDbContext _context;
        private readonly IMapper _mapper;

        public UserService(GradebookDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CustomResult> GetUser(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.Profiles)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return new CustomResult(new ErrorResult($"User with ID {id} not found.", ErrorCodes.ENTITY_NOT_FOUND));

            return new CustomResult<User>(user);
        }

        public async Task<CustomResult> GetAllUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.Profiles)
                .ToListAsync();

            return new CustomResult<IEnumerable<User>>(users);
        }

        public async Task<CustomResult> CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new CustomResult<User>(user);
        }

        public async Task<CustomResult> UpdateUser(Guid id, UserDto userDto)
        {
            if (id != userDto.Id)
                return new CustomResult(new ErrorResult("Mismatching IDs", ErrorCodes.ENTITY_MISMATCH_ID));

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return new CustomResult(new ErrorResult("User not found", ErrorCodes.ENTITY_NOT_FOUND));

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.FullName = userDto.FullName;
            user.IsActive = userDto.IsActive;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var details = result.Errors.Select(x => x.Description).ToList();
                var error = new ErrorResult("Updating user failed.", ErrorCodes.USER_UPDATE_FAILED, details);
            }

            return new CustomResult<User>(user);
        }

        public async Task<CustomResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return new CustomResult(new ErrorResult("User not found", ErrorCodes.ENTITY_NOT_FOUND));

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new CustomResult<string>("Deleted successfully!");
        }
    }
}
