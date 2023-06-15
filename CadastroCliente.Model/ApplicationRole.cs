using Microsoft.AspNetCore.Identity;

namespace CadastroCliente.Model
{
    public class ApplicationRole : IdentityRole<string>
    {
        // Adicione propriedades adicionais, se necessário

        // Adicione a chave estrangeira para a tabela AspNetRoles
        public string AspNetRoleId { get; set; }
        public IdentityRole AspNetRole { get; set; }
    }
}
