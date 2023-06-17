namespace CadastroCliente.Model
{
    public class Servico
    {
        public int Id { get; set; }
        public int OrdemDeServicoId { get; set; }
        public string Descricao { get; set; }
        public string Materiais { get; set; }
        public string Instrucoes { get; set; }
    }
}
