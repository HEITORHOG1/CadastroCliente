using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CadastroCliente.Api
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Verifica se a operação possui o atributo [AllowAnonymous]
            var hasAllowAnonymous = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>().Any()
                || context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

            if (hasAllowAnonymous)
            {
                return;
            }

            // Adiciona o esquema Bearer JWT como requisito de segurança para a operação
            var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "Bearer",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        };

            operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
        }
    }
}