namespace CadastroCliente.Model
{
    public class ClienteOrdemServicoModel
    {
        public Cliente Cliente { get; set; }
        public OrdemDeServico OrdemDeServico { get; set; }
        public Servico Servico { get; set; }
    }
}
