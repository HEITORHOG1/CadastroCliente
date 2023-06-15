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
        }

        public DbSet<User> Users { get; set; }
    }

}
