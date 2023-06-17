using CadastroCliente.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCliente.Services.Validators
{
    public class ServicoValidator : AbstractValidator<Servico>
    {
        public ServicoValidator()
        {
            RuleFor(servico => servico.Descricao)
                .NotEmpty().WithMessage("A descrição do serviço é obrigatória.")
                .Length(1, 500).WithMessage("A descrição do serviço deve ter entre 1 e 500 caracteres.");

            RuleFor(servico => servico.Materiais)
                .NotEmpty().WithMessage("A lista de materiais do serviço é obrigatória.")
                .Length(1, 500).WithMessage("A lista de materiais deve ter entre 1 e 500 caracteres.");

            RuleFor(servico => servico.Instrucoes)
                .NotEmpty().WithMessage("As instruções do serviço são obrigatórias.")
                .Length(1, 500).WithMessage("As instruções devem ter entre 1 e 500 caracteres.");
        }
    }
}
