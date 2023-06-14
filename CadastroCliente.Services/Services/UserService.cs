using CadastroCliente.Infra.IRepository;
using CadastroCliente.Model;
using CadastroCliente.Services.Validators;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CadastroCliente.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserValidator _validator;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, UserValidator validator, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<IEnumerable<User>> SearchUsersAsync(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                _logger.LogError("Search string cannot be null or empty");
                throw new ArgumentException("Search string cannot be null or empty", nameof(searchString));
            }

            var users = await _userRepository.SearchUsersAsync(searchString);

            if (!users.Any())
            {
                _logger.LogInformation($"No users found with search string: {searchString}");
            }

            return users;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _logger.LogInformation("Creating user with name {UserName}", user.Name);
            var validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for user with name {UserName}", user.Name);
                throw new ValidationException(validationResult.Errors);
            }

            var createdUser = await _userRepository.CreateUserAsync(user);
            _logger.LogInformation("Created user with ID {UserId}", createdUser.Id);
            return createdUser;
        }

        public async Task DeleteUserAsync(int id)
        {
            _logger.LogInformation("Deleting user with ID {UserId}", id);
            await _userRepository.DeleteUserAsync(id);
            _logger.LogInformation("Deleted user with ID {UserId}", id);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            _logger.LogInformation("Getting user with ID {UserId}", id);
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", id);
            }
            else
            {
                _logger.LogInformation("Retrieved user with ID {UserId}", id);
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            _logger.LogInformation("Getting all users");
            var users = await _userRepository.GetUsersAsync();
            _logger.LogInformation("Retrieved {UserCount} users", users?.Count());
            return users;
        }


        public async Task<User> UpdateUserAsync(User user)
        {
            _logger.LogInformation("Updating user with ID {UserId}", user.Id);
            var validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for user with ID {UserId}", user.Id);
                throw new ValidationException(validationResult.Errors);
            }

            var updatedUser = await _userRepository.UpdateUserAsync(user);
            _logger.LogInformation("Updated user with ID {UserId}", user.Id);
            return updatedUser;
        }
    }
}
