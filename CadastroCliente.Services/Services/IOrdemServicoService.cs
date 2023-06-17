using CadastroCliente.Infra.Migrations;
using CadastroCliente.Model;

namespace CadastroCliente.Services.Services
{
    public interface IOrdemServicoService
    {
        Task<IEnumerable<OrdemDeServico>> GetOrdemsAsync();
        Task<OrdemDeServico> GetOrdemByIdAsync(int id);
        Task<OrdemDeServico> CreateOrdemAsync(OrdemDeServico user);
        Task<OrdemDeServico> UpdateOrdemAsync(OrdemDeServico user);
        Task DeleteOrdemAsync(int id);
    }
}
