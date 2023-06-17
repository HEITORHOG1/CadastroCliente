namespace CadastroCliente.Web.Models
{
    public class OrdemDeServicoDTO
    {
        public OrdemDeServicoDTO()
        {
            // Gerar um número aleatório entre 1000 e 9999
            Random random = new Random();
            Numero = random.Next(1000, 10000).ToString();
        }

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
