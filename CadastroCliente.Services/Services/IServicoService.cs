using CadastroCliente.Model;

namespace CadastroCliente.Services.Services
{
    public interface IServicoService
    {
        Task<IEnumerable<Servico>> GetUsersAsync();
        Task<Servico> GetUserByIdAsync(int id);
        Task<Servico> CreateUserAsync(Servico user);
        Task<Servico> UpdateUserAsync(Servico user);
        Task DeleteUserAsync(int id);
    }
}
