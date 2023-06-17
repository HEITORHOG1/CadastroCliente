namespace CadastroCliente.Web.Models
{
    public class ServicoDTO
    {
        public int Id { get; set; }
        public int OrdemDeServicoId { get; set; }
        public string Descricao { get; set; }
        public string Materiais { get; set; }
        public string Instrucoes { get; set; }
    }
}
