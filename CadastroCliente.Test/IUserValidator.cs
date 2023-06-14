using CadastroCliente.Model;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCliente.Test
{
    public interface IUserValidator
    {
        Task<ValidationResult> ValidateAsync(User entity);
    }

}
