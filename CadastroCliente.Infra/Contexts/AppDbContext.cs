using Microsoft.EntityFrameworkCore;
using CadastroCliente.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CadastroCliente.Infra.Contexts
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurações personalizadas do modelo aqui

            // Configura o relacionamento entre ApplicationUser e ApplicationRole
            builder.Entity<ApplicationRole>()
                .HasOne(r => r.AspNetRole)
                .WithMany()
                .HasForeignKey(r => r.AspNetRoleId);

            // Configuração das tabelas "OrdemDeServico", "Cliente" e "Servico"
            //builder.Entity<OrdemDeServico>()
            //    .HasOne(o => o.Cliente)
            //    .WithMany(c => c.OrdensDeServico)
            //    .HasForeignKey(o => o.ClienteId);

            //builder.Entity<OrdemDeServico>()
            //    .HasMany(o => o.Servicos)
            //    .WithOne(s => s.OrdemDeServico)
            //    .HasForeignKey(s => s.OrdemDeServicoId);

            //builder.Entity<Servico>()
            //    .HasOne(s => s.OrdemDeServico)
            //    .WithMany(o => o.Servicos)
            //    .HasForeignKey(s => s.OrdemDeServicoId);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<OrdemDeServico> OrdensDeServico { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
    }

}
