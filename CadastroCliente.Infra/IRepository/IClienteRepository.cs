using CadastroCliente.Model;

namespace CadastroCliente.Infra.IRepository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteOrdemServicoModel>> GetUsersAsync(string search = null);
        Task<IEnumerable<ClienteOrdemServicoModel>> GetUsersAsync();
        Task<ClienteOrdemServicoModel> GetUserByIdAsync(int id);
        Task<ClienteOrdemServicoModel> CreateUserAsync(ClienteOrdemServicoModel user);
        Task<ClienteOrdemServicoModel> UpdateUserAsync(ClienteOrdemServicoModel user);
        Task DeleteUserAsync(int id);
    }
}
