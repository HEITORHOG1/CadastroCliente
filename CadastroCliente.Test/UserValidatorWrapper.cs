using CadastroCliente.Model;
using CadastroCliente.Services.Validators;
using FluentValidation.Results;

namespace CadastroCliente.Test
{
    public class UserValidatorWrapper : IUserValidator
    {
        private readonly UserValidator _validator;

        public UserValidatorWrapper(UserValidator validator)
        {
            _validator = validator;
        }

        public Task<ValidationResult> ValidateAsync(User entity)
        {
            return _validator.ValidateAsync(entity);
        }
    }

}
