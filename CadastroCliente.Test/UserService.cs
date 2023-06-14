using CadastroCliente.Infra.IRepository;
using CadastroCliente.Model;
using FluentValidation;

namespace CadastroCliente.Test
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _validator;

        public UserService(IUserRepository userRepository, IUserValidator validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _userRepository.UpdateUserAsync(user);
        }
    }

}
