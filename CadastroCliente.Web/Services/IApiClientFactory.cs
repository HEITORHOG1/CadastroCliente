namespace CadastroCliente.Web.Services
{
    public interface IApiClientFactory
    {
        ApiClient Create(string jwtToken);
    }

}
