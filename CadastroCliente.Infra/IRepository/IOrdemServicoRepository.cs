using CadastroCliente.Infra.Migrations;
using CadastroCliente.Model;

namespace CadastroCliente.Infra.IRepository
{
    public interface IOrdemServicoRepository
    {
        Task<IEnumerable<OrdemDeServico>> GetOrdemsAsync();
        Task<OrdemDeServico> GetOrdemByIdAsync(int id);
        Task<OrdemDeServico> CreateOrdemAsync(OrdemDeServico user);
        Task<OrdemDeServico> UpdateOrdemAsync(OrdemDeServico user);
        Task DeleteOrdemAsync(int id);
    }
}
