using CadastroCliente.Infra.Contexts;
using CadastroCliente.Infra.IRepository;
using CadastroCliente.Infra.Migrations;
using CadastroCliente.Model;
using Microsoft.EntityFrameworkCore;

namespace CadastroCliente.Infra.Repository
{
    public class OrdemServicoRepository : IOrdemServicoRepository
    {
        private readonly AppDbContext _context;

        public OrdemServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrdemDeServico> CreateOrdemAsync(OrdemDeServico ordemServico)
        {
            _context.OrdensDeServico.Add(ordemServico);

            // Add each Servico to the DbContext

            await _context.SaveChangesAsync();
            return ordemServico;
        }


        public async Task DeleteOrdemAsync(int id)
        {
            var ordemServico = await _context.OrdensDeServico.FindAsync(id);
            if (ordemServico != null)
            {
                _context.OrdensDeServico.Remove(ordemServico);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<OrdemDeServico> GetOrdemByIdAsync(int id)
        {
            return await _context.OrdensDeServico.FindAsync(id);
        }

        public async Task<IEnumerable<OrdemDeServico>> GetOrdemsAsync()
        {
            return await _context.OrdensDeServico.ToListAsync();
        }

        public async Task<OrdemDeServico> UpdateOrdemAsync(OrdemDeServico ordemServico)
        {
            var existingOrdemServico = await _context.OrdensDeServico.FindAsync(ordemServico.Id);
            if (existingOrdemServico == null) return null;

            _context.Entry(existingOrdemServico).CurrentValues.SetValues(ordemServico);


            await _context.SaveChangesAsync();

            return existingOrdemServico;
        }

    }
}
