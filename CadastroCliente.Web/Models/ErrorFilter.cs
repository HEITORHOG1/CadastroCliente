using Microsoft.AspNetCore.Mvc.Filters;

namespace CadastroCliente.Web.Models
{
    public class ErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errorMessage = context.Exception.Message;
            context.HttpContext.Items["ErrorMessage"] = errorMessage;
        }
    }
}
