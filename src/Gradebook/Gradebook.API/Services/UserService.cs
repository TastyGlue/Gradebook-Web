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
            var user = userDto.Adapt<User>();
            if (string.IsNullOrWhiteSpace(user.UserName))
                user.UserName = user.Email; // Ensure UserName is set if not provided

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                var details = result.Errors.Select(x => x.Description).ToList();
                var error = new ErrorResult("Creating user failed.", ErrorCodes.USER_CREATE_FAILED, details);
            }

            return new CustomResult<User>(user);
        }

        public async Task<CustomResult> UpdateUser(Guid id, UserDto userDto)
        {
            if (id != userDto.Id)
                return new CustomResult(new ErrorResult("Mismatching IDs", ErrorCodes.ENTITY_MISMATCH_ID));

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return new CustomResult(new ErrorResult("User not found", ErrorCodes.ENTITY_NOT_FOUND));

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

        public async Task<CustomResult> ResetPassword(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return new CustomResult(new ErrorResult("User not found", ErrorCodes.ENTITY_NOT_FOUND));
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string password = GeneratePassword();

            var resetResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (!resetResult.Succeeded)
            {
                var details = resetResult.Errors.Select(x => x.Description).ToList();
                return new CustomResult(new ErrorResult("Password reset failed.", ErrorCodes.USER_UPDATE_FAILED, details));
            }

            return new CustomResult<string>(password);
        }

        private string GeneratePassword()
        {
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            const string allChars = lowercase + uppercase + digits;

            Random rand = new Random();

            // Ensure at least one of each required character type
            char[] password = new char[10];
            password[0] = lowercase[rand.Next(lowercase.Length)];
            password[1] = uppercase[rand.Next(uppercase.Length)];
            password[2] = digits[rand.Next(digits.Length)];

            // Fill the rest with random characters from all available characters
            for (int i = 3; i < 10; i++)
            {
                password[i] = allChars[rand.Next(allChars.Length)];
            }

            // Shuffle the password so required characters are not in fixed positions
            return new string(password.OrderBy(x => rand.Next()).ToArray());
        }
    }
}