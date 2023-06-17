using CadastroCliente.Model;

namespace CadastroCliente.Services.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteOrdemServicoModel>> GetUsersAsync(string search = null);
        Task<IEnumerable<ClienteOrdemServicoModel>> GetUsersAsync();
        Task<ClienteOrdemServicoModel> GetUserByIdAsync(int id);
        Task<ClienteOrdemServicoModel> CreateUserAsync(ClienteOrdemServicoModel user);
        Task<ClienteOrdemServicoModel> UpdateUserAsync(ClienteOrdemServicoModel user);
        Task DeleteUserAsync(int id);
    }
}
