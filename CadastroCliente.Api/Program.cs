using CadastroCliente.Api.Infra;
using CadastroCliente.Infra.Contexts;
using CadastroCliente.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CadastroCliente.Api;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 0;
    });

string cepUrl = builder.Configuration["CepUrl"];

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructure();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 21)))); // replace the version with your MySQL server version

builder.Services.AddIdentityCore<ApplicationUser>() // Use AddIdentityCore instead of AddDefaultIdentity
    .AddRoles<IdentityRole>()  // if you want to use roles
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["JwtIssuer"], // Altere para configuration["JwtIssuer"]
        ValidAudience = configuration["JwtAudience"], // Altere para configuration["JwtAudience"]
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])) // Altere para configuration["JwtKey"]
    };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CadastroCliente API",
        Description = "API for managing client registrations",
        TermsOfService = new Uri("http://br.linkedin.com/in/heitorhog"),
        Contact = new OpenApiContact
        {
            Name = "Heitor Oliveira Gonçalves",
            Email = "heitorhog@gmail.com",
            Url = new Uri("http://br.linkedin.com/in/heitorhog"),
        },
        License = new OpenApiLicense
        {
            Name = "Acesse meu Linkedin",
            Url = new Uri("http://br.linkedin.com/in/heitorhog"),
        }
    });

    // Adicione a operação de segurança para exigir o esquema Bearer JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    // Adicione o requisito de segurança para todas as operações
    c.OperationFilter<SecurityRequirementsOperationFilter>();
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

app.UseAuthentication(); // add this line
app.UseAuthorization();

app.MapControllers();

app.Run();
