using System.ComponentModel.DataAnnotations;

namespace CadastroCliente.Web.Models
{
    public class LoginModelDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
