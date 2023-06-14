using CadastroCliente.Api.Infra;
using CadastroCliente.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructure();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 21)))); // replace the version with your MySQL server version

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CadastroCliente API",
        Description = "API for managing client registrations",
        TermsOfService = new Uri(" http://br.linkedin.com/in/heitorhog"),
        Contact = new OpenApiContact
        {
            Name = "Heitor Oliveira Gonçalves",
            Email = "heitorhog@gmail.com",
            Url = new Uri(" http://br.linkedin.com/in/heitorhog"),
        },
        License = new OpenApiLicense
        {
            Name = "Acesse meu Linkedin",
            Url = new Uri(" http://br.linkedin.com/in/heitorhog"),
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CadastroCliente API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
