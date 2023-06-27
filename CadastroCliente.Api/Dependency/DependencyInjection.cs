
using CadastroCliente.Infra.Contexts;
using CadastroCliente.Infra.IRepository;
using CadastroCliente.Infra.Repository;
using CadastroCliente.Model;
using CadastroCliente.Services.Services;
using CadastroCliente.Services.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

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
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IServicoRepository, ServicoRepository>();
            services.AddScoped<IServicoService, ServicoService>();
            services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
            services.AddScoped<IOrdemServicoService, OrdemServicoService>();
            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<IValidator<ClienteOrdemServicoModel>, ClienteOrdemServicoModelValidator>();
            services.AddTransient<IValidator<OrdemDeServico>, OrdemDeServicoValidator>();
            services.AddTransient<IValidator<Servico>, ServicoValidator>();
            services.AddTransient<IValidator<Cliente>, ClienteValidator>();
            services.AddTransient<UserValidator>();
            services.AddTransient<ClienteOrdemServicoModelValidator>();
            services.AddTransient<OrdemDeServicoValidator>();
            services.AddTransient<ServicoValidator>();
            services.AddTransient<ClienteValidator>();
            services.AddScoped<IApiConfigService, ApiConfigService>();
            services.AddScoped<ICepService, CepService>();
            services.AddHttpClient<ICepService, CepService>();

            // Add Identity services
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

    }
}

