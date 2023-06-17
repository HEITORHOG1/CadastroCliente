using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCliente.Services.Validators
{
    public class SearchValidator : AbstractValidator<string>
    {
        public SearchValidator()
        {
            RuleFor(search => search)
                .MaximumLength(100).WithMessage("Search term must be less than 100 characters.");
        }
    }

}
