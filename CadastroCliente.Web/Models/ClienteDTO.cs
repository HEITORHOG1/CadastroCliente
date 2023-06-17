namespace CadastroCliente.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ClienteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo Nome deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo Endereço deve ter no máximo {1} caracteres.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        [StringLength(20, ErrorMessage = "O campo Telefone deve ter no máximo {1} caracteres.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email não é um endereço de email válido.")]
        public string Email { get; set; }
    }

}
