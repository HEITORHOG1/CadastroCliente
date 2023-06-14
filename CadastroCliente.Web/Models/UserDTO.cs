using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CadastroCliente.Web.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [Display(Name = "Número de Telefone")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Código Postal")]
        public string PostalCode { get; set; }
    }
}
