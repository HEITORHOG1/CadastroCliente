using CadastroCliente.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCliente.Services.Validators
{
    public class OrdemDeServicoValidator : AbstractValidator<OrdemDeServico>
    {
        public OrdemDeServicoValidator()
        {
            RuleFor(ordem => ordem.Numero)
                .NotEmpty().WithMessage("O número da ordem de serviço é obrigatório.")
                .Length(1, 50).WithMessage("O número da ordem de serviço deve ter entre 1 e 50 caracteres.");

            RuleFor(ordem => ordem.DataEmissao)
                .NotEmpty().WithMessage("A data de emissão é obrigatória.");

            RuleFor(ordem => ordem.Responsavel)
                .NotEmpty().WithMessage("O responsável pela ordem de serviço é obrigatório.")
                .Length(1, 100).WithMessage("O nome do responsável deve ter entre 1 e 100 caracteres.");

            RuleFor(ordem => ordem.PrazoExecucao)
                .NotEmpty().WithMessage("O prazo de execução é obrigatório.");

            RuleFor(ordem => ordem.DataConclusao)
                .NotEmpty().WithMessage("A data de conclusão é obrigatória.")
                .GreaterThanOrEqualTo(ordem => ordem.DataEmissao)
                .WithMessage("A data de conclusão não pode ser anterior à data de emissão.");
        }
    }
}
