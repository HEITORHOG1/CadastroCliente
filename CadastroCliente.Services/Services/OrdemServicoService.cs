using CadastroCliente.Infra.IRepository;
using CadastroCliente.Model;

namespace CadastroCliente.Services.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepository;

        public OrdemServicoService(IOrdemServicoRepository ordemServicoRepository)
        {
            _ordemServicoRepository = ordemServicoRepository;
        }

        public async Task<OrdemDeServico> CreateOrdemAsync(OrdemDeServico ordemServico)
        {
            return await _ordemServicoRepository.CreateOrdemAsync(ordemServico);
        }

        public async Task DeleteOrdemAsync(int id)
        {
            await _ordemServicoRepository.DeleteOrdemAsync(id);
        }

        public async Task<OrdemDeServico> GetOrdemByIdAsync(int id)
        {
            return await _ordemServicoRepository.GetOrdemByIdAsync(id);
        }

        public async Task<IEnumerable<OrdemDeServico>> GetOrdemsAsync()
        {
            return await _ordemServicoRepository.GetOrdemsAsync();
        }

        public async Task<OrdemDeServico> UpdateOrdemAsync(OrdemDeServico ordemServico)
        {
            return await _ordemServicoRepository.UpdateOrdemAsync(ordemServico);
        }
    }
}
