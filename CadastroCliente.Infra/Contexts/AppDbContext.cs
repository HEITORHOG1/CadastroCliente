using Microsoft.EntityFrameworkCore;
using CadastroCliente.Model;

namespace CadastroCliente.Infra.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }

}
