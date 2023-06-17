using CadastroCliente.Infra.IRepository;
using CadastroCliente.Model;
using Microsoft.Extensions.Logging;

namespace CadastroCliente.Services.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _servicoRepository;
        private readonly ILogger<UserService> _logger;
        public ServicoService(IServicoRepository servicoRepository , ILogger<UserService> logger)
        {
            _servicoRepository = servicoRepository;
            _logger = logger;
        }

        public async Task<Servico> CreateUserAsync(Servico servico)
        {
            return await _servicoRepository.CreateServicoAsync(servico);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _servicoRepository.DeleteServicoAsync(id);
        }

        public async Task<Servico> GetUserByIdAsync(int id)
        {
            return await _servicoRepository.GetServicoByIdAsync(id);
        }

        public async Task<IEnumerable<Servico>> GetUsersAsync()
        {
            return await _servicoRepository.GetServicosAsync();
        }

        public async Task<Servico> UpdateUserAsync(Servico servico)
        {
            return await _servicoRepository.UpdateServicoAsync(servico);
        }
    }
}
