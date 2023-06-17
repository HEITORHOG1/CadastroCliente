using CadastroCliente.Model;
using FluentValidation;

namespace CadastroCliente.Services.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(cliente => cliente.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(cliente => cliente.Endereco)
                .NotEmpty().WithMessage("O endereço é obrigatório.")
                .Length(1, 200).WithMessage("O endereço deve ter entre 1 e 200 caracteres.");

            RuleFor(cliente => cliente.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Length(10, 15).WithMessage("O telefone deve ter entre 10 e 15 dígitos.")
                .Matches(@"^(\+[0-9]{1,3})? ?([0-9]{1,15})$").WithMessage("Telefone inválido.");

            RuleFor(cliente => cliente.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");
        }
    }
}
