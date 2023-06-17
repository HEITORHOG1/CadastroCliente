using System.Text.Json.Serialization;

namespace CadastroCliente.Model
{
    public class OrdemDeServico
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime PrazoExecucao { get; set; }
        public string Responsavel { get; set; }
        public DateTime? DataConclusao { get; set; }
        public decimal? Valor { get; set; }
    }
}
