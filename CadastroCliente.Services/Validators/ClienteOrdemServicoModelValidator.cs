using CadastroCliente.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCliente.Services.Validators
{
    public class ClienteOrdemServicoModelValidator : AbstractValidator<ClienteOrdemServicoModel>
    {
        public ClienteOrdemServicoModelValidator()
        {
            RuleFor(model => model.Cliente)
                .NotEmpty().WithMessage("Os dados do cliente são obrigatórios.")
                .SetValidator(new ClienteValidator());

            RuleFor(model => model.OrdemDeServico)
                .NotEmpty().WithMessage("Os dados da ordem de serviço são obrigatórios.")
                .SetValidator(new OrdemDeServicoValidator());

            RuleFor(model => model.Servico)
                .NotEmpty().WithMessage("Os dados do serviço são obrigatórios.")
                .SetValidator(new ServicoValidator());
        }
    }
}
