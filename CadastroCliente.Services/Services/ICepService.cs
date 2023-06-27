using CadastroCliente.Model;

namespace CadastroCliente.Services.Services
{
    public interface ICepService
    {
        Task<Cep> GetCep(string cep);
    }
}
