using CadastroCliente.Infra.IRepository;
using CadastroCliente.Infra.Repository;
using CadastroCliente.Model;
using CadastroCliente.Services.Services;
using CadastroCliente.Services.Validators;
using FluentValidation;

namespace CadastroCliente.Api.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<UserValidator>();
            return services;
        }
    }
}

