using CadastroCliente.Infra.Contexts;
using CadastroCliente.Infra.IRepository;
using CadastroCliente.Model;
using Microsoft.EntityFrameworkCore;

namespace CadastroCliente.Infra.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClienteOrdemServicoModel>> GetUsersAsync(string search = null)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Nome.Contains(search) || c.Email.Contains(search) || c.Telefone.Contains(search));
            }

            return await query
                .Select(c => new ClienteOrdemServicoModel
                {
                    Cliente = c,
                    OrdemDeServico = _context.OrdensDeServico.FirstOrDefault(o => o.ClienteId == c.Id),
                    Servico = _context.Servicos.FirstOrDefault(s => s.OrdemDeServicoId == _context.OrdensDeServico.FirstOrDefault(o => o.ClienteId == c.Id).Id)
                })
                .ToListAsync();
        }

        public async Task<ClienteOrdemServicoModel> CreateUserAsync(ClienteOrdemServicoModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Cria o Cliente.
                    _context.Clientes.Add(model.Cliente);
                    await _context.SaveChangesAsync();

                    // Atualiza a OrdemDeServico com o Id do Cliente criado.
                    model.OrdemDeServico.ClienteId = model.Cliente.Id;
                    _context.OrdensDeServico.Add(model.OrdemDeServico);
                    await _context.SaveChangesAsync();

                    // Atualiza o Servico com o Id da OrdemDeServico criada.
                    model.Servico.OrdemDeServicoId = model.OrdemDeServico.Id;
                    _context.Servicos.Add(model.Servico);
                    await _context.SaveChangesAsync();

                    // Commita a transação.
                    await transaction.CommitAsync();

                    return model;
                }
                catch (Exception)
                {
                    // Desfaz as alterações se algo der errado.
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task DeleteUserAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                var ordensDeServico = await _context.OrdensDeServico.Where(o => o.ClienteId == id).ToListAsync();
                var servicos = await _context.Servicos.Where(s => ordensDeServico.Select(o => o.Id).Contains(s.OrdemDeServicoId)).ToListAsync();

                _context.Servicos.RemoveRange(servicos);
                _context.OrdensDeServico.RemoveRange(ordensDeServico);
                _context.Clientes.Remove(cliente);

                await _context.SaveChangesAsync();
            }
        }


        public async Task<ClienteOrdemServicoModel> GetUserByIdAsync(int id)
        {
            return await _context.Clientes
                        .Where(c => c.Id == id)
                        .Select(c => new ClienteOrdemServicoModel
                        {
                            Cliente = c,
                            OrdemDeServico = _context.OrdensDeServico.FirstOrDefault(o => o.ClienteId == c.Id),
                            Servico = _context.Servicos.FirstOrDefault(s => s.OrdemDeServicoId == _context.OrdensDeServico.FirstOrDefault(o => o.ClienteId == c.Id).Id)
                        })
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ClienteOrdemServicoModel>> GetUsersAsync()
        {
            return await _context.Clientes
                        .Select(c => new ClienteOrdemServicoModel
                        {
                            Cliente = c,
                            OrdemDeServico = _context.OrdensDeServico.FirstOrDefault(o => o.ClienteId == c.Id),
                            Servico = _context.Servicos.FirstOrDefault(s => s.OrdemDeServicoId == _context.OrdensDeServico.FirstOrDefault(o => o.ClienteId == c.Id).Id)
                        })
                        .ToListAsync();
        }


        public async Task<ClienteOrdemServicoModel> UpdateUserAsync(ClienteOrdemServicoModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Busca as entidades existentes pelo ID.
                    var clienteExistente = await _context.Clientes.FindAsync(model.Cliente.Id);
                    var ordemDeServicoExistente = await _context.OrdensDeServico.FindAsync(model.OrdemDeServico.Id);
                    var servicoExistente = await _context.Servicos.FindAsync(model.Servico.Id);

                    // Atualiza as propriedades das entidades existentes.
                    _context.Entry(clienteExistente).CurrentValues.SetValues(model.Cliente);
                    _context.Entry(ordemDeServicoExistente).CurrentValues.SetValues(model.OrdemDeServico);
                    _context.Entry(servicoExistente).CurrentValues.SetValues(model.Servico);

                    // Salva as alterações.
                    await _context.SaveChangesAsync();

                    // Commita a transação.
                    await transaction.CommitAsync();

                    return model;
                }
                catch (Exception)
                {
                    // Desfaz as alterações se algo der errado.
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }


    }
}
