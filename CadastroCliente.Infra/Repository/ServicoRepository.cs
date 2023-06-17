using CadastroCliente.Infra.Contexts;
using CadastroCliente.Infra.IRepository;
using CadastroCliente.Model;
using Microsoft.EntityFrameworkCore;

namespace CadastroCliente.Infra.Repository
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly AppDbContext _context;

        public ServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Servico> CreateServicoAsync(Servico servico)
        {
            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();
            return servico;
        }

        public async Task DeleteServicoAsync(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico != null)
            {
                _context.Servicos.Remove(servico);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Servico> GetServicoByIdAsync(int id)
        {
            return await _context.Servicos.FindAsync(id);
        }

        public async Task<IEnumerable<Servico>> GetServicosAsync()
        {
            return await _context.Servicos.ToListAsync();
        }

        public async Task<Servico> UpdateServicoAsync(Servico servico)
        {
            var existingServico = await _context.Servicos.FindAsync(servico.Id);
            if (existingServico == null) return null;

            _context.Entry(existingServico).CurrentValues.SetValues(servico);
            await _context.SaveChangesAsync();

            return existingServico;
        }
    }
}
