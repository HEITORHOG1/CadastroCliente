using CadastroCliente.Model;

namespace CadastroCliente.Infra.IRepository
{
    public interface IServicoRepository
    {
        Task<IEnumerable<Servico>> GetServicosAsync();
        Task<Servico> GetServicoByIdAsync(int id);
        Task<Servico> CreateServicoAsync(Servico user);
        Task<Servico> UpdateServicoAsync(Servico user);
        Task DeleteServicoAsync(int id);
    }
}
