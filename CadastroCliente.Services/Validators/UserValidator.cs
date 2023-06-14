using CadastroCliente.Model;
using FluentValidation;

namespace CadastroCliente.Services.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(user => user.Address)
                .NotEmpty().WithMessage("O endereço é obrigatório.")
                .Length(1, 200).WithMessage("O endereço deve ter entre 1 e 200 caracteres.");

            RuleFor(user => user.PhoneNumber)
                .NotEmpty().WithMessage("O número de telefone é obrigatório.")
                .Length(10, 15).WithMessage("O número de telefone deve ter entre 10 e 15 dígitos.")
                .Matches(@"^(\+[0-9]{1,3})? ?([0-9]{1,15})$").WithMessage("Número de telefone inválido.");

            RuleFor(user => user.PostalCode)
                .NotEmpty().WithMessage("O código postal é obrigatório.")
                .Length(5, 10).WithMessage("O código postal deve ter entre 5 e 10 caracteres.")
                .Matches(@"^[0-9a-zA-Z]+$").WithMessage("Código postal inválido. Deve ser composto por números e/ou letras.");
        }
    }

}
