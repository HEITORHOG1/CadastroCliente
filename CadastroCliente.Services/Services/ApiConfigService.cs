using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCliente.Services.Services
{
    public class ApiConfigService : IApiConfigService
    {
        private readonly IConfiguration _configuration;

        public ApiConfigService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetCepUrl()
        {
            return _configuration["CepUrl"];
        }
    }
}
