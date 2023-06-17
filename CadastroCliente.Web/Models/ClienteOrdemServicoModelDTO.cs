using CadastroCliente.Model;

namespace CadastroCliente.Web.Models
{
    public class ClienteOrdemServicoModelDTO
    {
        public ClienteOrdemServicoModelDTO()
        {
            Cliente = new ClienteDTO();
            OrdemDeServico = new OrdemDeServicoDTO();
            Servico = new ServicoDTO();
        }
        public ClienteDTO Cliente { get; set; }
        public OrdemDeServicoDTO OrdemDeServico { get; set; }
        public ServicoDTO Servico { get; set; }
    }
}
