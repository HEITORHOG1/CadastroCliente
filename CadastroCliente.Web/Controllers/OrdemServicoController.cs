using AutoMapper;
using CadastroCliente.Web.Models;
using CadastroCliente.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CadastroCliente.Web.Controllers
{
    public class OrdemServicoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly string _jwtToken;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly ILogger<OrdemServicoController> _logger;

        public OrdemServicoController(IApiClientFactory apiClientFactory, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<OrdemServicoController> logger)
        {
            _apiClientFactory = apiClientFactory;
            _mapper = mapper;
            _jwtToken = httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _logger = logger;
        }

        public async Task<IActionResult> Pesquisar(string termoBusca)
        {
            try
            {
                var apiClient = _apiClientFactory.Create(_jwtToken);
                IEnumerable<ClienteOrdemServicoModelDTO> ordemServicos;

                if (string.IsNullOrEmpty(termoBusca))
                {
                    ordemServicos = await apiClient.GetAsync<IEnumerable<ClienteOrdemServicoModelDTO>>("api/Cliente");
                }
                else
                {
                    ordemServicos = await apiClient.GetAsync<IEnumerable<ClienteOrdemServicoModelDTO>>($"api/Cliente/Buscar?termoBusca={termoBusca}");
                }

                if (ordemServicos == null || !ordemServicos.Any())
                {
                    return View("Index");
                }

                return View("Index", ordemServicos);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ocorreu um erro durante a busca dos clientes.");
                TempData["ErrorMessage"] = "Não foi possível buscar os clientes no momento. Por favor, tente novamente mais tarde.";
                return View("Index");
            }
        }



        public async Task<IActionResult> Index()
        {
            try
            {
                var apiClient = _apiClientFactory.Create(_jwtToken);
                var ordemServicos = await apiClient.GetAsync<IEnumerable<ClienteOrdemServicoModelDTO>>("api/Cliente");
                return View(ordemServicos);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao buscar os serviços de cliente.");
                TempData["ErrorMessage"] = "Não foi possível buscar os serviços de cliente no momento. Por favor, tente novamente mais tarde.";
                return View(new List<ClienteOrdemServicoModelDTO>()); // Retorna a view com uma lista vazia
            }
        }


        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var apiClient = _apiClientFactory.Create(_jwtToken);
                var ordemDeServico = await apiClient.GetAsync<ClienteOrdemServicoModelDTO>($"api/Cliente/{id}");

                if (ordemDeServico == null)
                {
                    return NotFound();
                }

                return View("Get", ordemDeServico);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro ao buscar a ordem de serviço com ID {id}.");
                TempData["ErrorMessage"] = $"Não foi possível buscar a ordem de serviço no momento. Por favor, tente novamente mais tarde.";
                return View("Error"); // Retorna a view de erro
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var apiClient = _apiClientFactory.Create(_jwtToken);
                var ordemDeServico = await apiClient.GetAsync<ClienteOrdemServicoModelDTO>($"api/Cliente/{id}");

                if (ordemDeServico == null)
                {
                    return NotFound();
                }

                return View("Details", ordemDeServico);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro ao buscar os detalhes da ordem de serviço com ID {id}.");
                TempData["ErrorMessage"] = $"Não foi possível buscar os detalhes da ordem de serviço no momento. Por favor, tente novamente mais tarde.";
                return View("Error"); // Retorna a view de erro
            }
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            try
            {
                DateTime.TryParse(formCollection["OrdemDeServico.DataEmissao"], out var dataEmissao);
                DateTime.TryParse(formCollection["OrdemDeServico.PrazoExecucao"], out var prazoExecucao);

                // Se não conseguir analisar a DataConclusao ou for vazia, define como DateTime.MinValue
                DateTime dataConclusao;
                if (!DateTime.TryParse(formCollection["OrdemDeServico.DataConclusao"], out dataConclusao) || string.IsNullOrEmpty(formCollection["OrdemDeServico.DataConclusao"]))
                {
                    dataConclusao = DateTime.MinValue;
                }

                var ordemDeServico = new ClienteOrdemServicoModelDTO
                {
                    Cliente = new ClienteDTO
                    {
                        Nome = formCollection["Cliente.Nome"],
                        Telefone = formCollection["Cliente.Telefone"],
                        Endereco = formCollection["Cliente.Endereco"],
                        Email = formCollection["Cliente.Email"]
                    },
                    OrdemDeServico = new OrdemDeServicoDTO
                    {
                        // Numero = formCollection["OrdemDeServico.Numero"],
                        DataEmissao = dataEmissao,
                        Responsavel = formCollection["OrdemDeServico.Responsavel"],
                        PrazoExecucao = prazoExecucao,
                        DataConclusao = dataConclusao
                    },
                    Servico = new ServicoDTO
                    {
                        Descricao = formCollection["Servico.Descricao"],
                        Materiais = formCollection["Servico.Materiais"],
                        Instrucoes = formCollection["Servico.Instrucoes"]
                    }
                };

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Received an invalid User creation request");
                    return View("Create", ordemDeServico);
                }

                var apiClient = _apiClientFactory.Create(_jwtToken);
                _logger.LogInformation("Sending a request to create a new User");
                var response = await apiClient.PostAsync("api/Cliente", ordemDeServico);

                if (!response.IsSuccessStatusCode)
                {
                    // Leia o conteúdo de erro como uma string
                    var errorContent = await response.Content.ReadAsStringAsync();

                    // Adicione uma mensagem de erro ao ModelState
                    ModelState.AddModelError("", errorContent);
                    ViewBag.ErrorMessage = errorContent;

                    _logger.LogError("Failed to create a new User");
                    return View("Create", ordemDeServico);
                }

                _logger.LogInformation("Successfully created a new User, redirecting to Index");
                return RedirectToAction(nameof(Index));

            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "An error occurred while trying to create a new User.");
                TempData["ErrorMessage"] = "Ocorreu um erro ao tentar criar um novo usuário. Por favor, tente novamente mais tarde.";
                return View("Error"); // Retorna a view de erro
            }
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var apiClient = _apiClientFactory.Create(_jwtToken);
                var ordemDeServico = await apiClient.GetAsync<ClienteOrdemServicoModelDTO>($"api/Cliente/{id}");

                if (ordemDeServico == null)
                {
                    return NotFound();
                }

                return View("Update", ordemDeServico);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"An error occurred while trying to update the User with ID {id}.");
                TempData["ErrorMessage"] = "Ocorreu um erro ao tentar atualizar o usuário. Por favor, tente novamente mais tarde.";
                return View("Error"); // Retorna a view de erro
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, IFormCollection formCollection)
        {
            try
            {
                var ordemDeServico = new ClienteOrdemServicoModelDTO
                {
                    Cliente = new ClienteDTO
                    {
                        Id = int.Parse(formCollection["Cliente.Id"]),
                        Nome = formCollection["Cliente.Nome"],
                        Telefone = formCollection["Cliente.Telefone"],
                        Endereco = formCollection["Cliente.Endereco"],
                        Email = formCollection["Cliente.Email"]
                    },
                    OrdemDeServico = new OrdemDeServicoDTO
                    {
                        ClienteId = int.Parse(formCollection["Cliente.Id"]),
                        Id = int.Parse(formCollection["OrdemDeServico.Id"]),
                        Numero = formCollection["OrdemDeServico.Numero"],
                        DataEmissao = DateTime.Parse(formCollection["OrdemDeServico.DataEmissao"]),
                        Responsavel = formCollection["OrdemDeServico.Responsavel"],
                        PrazoExecucao = DateTime.Parse(formCollection["OrdemDeServico.PrazoExecucao"]),
                        DataConclusao = DateTime.Parse(formCollection["OrdemDeServico.DataConclusao"])
                    },
                    Servico = new ServicoDTO
                    {
                        Id = int.Parse(formCollection["Servico.Id"]),
                        OrdemDeServicoId = int.Parse(formCollection["Servico.OrdemDeServicoId"]),
                        Descricao = formCollection["Servico.Descricao"],
                        Materiais = formCollection["Servico.Materiais"],
                        Instrucoes = formCollection["Servico.Instrucoes"]
                    }
                };

                try
                {
                    _logger.LogInformation("Sending a request to update a User");
                    var apiClient = _apiClientFactory.Create(_jwtToken);

                    var response = await apiClient.PutAsync($"api/Cliente/{id}", ordemDeServico);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();

                        ViewBag.ErrorMessage = errorContent;

                        return View("Update", ordemDeServico);
                    }
                    else
                    {
                        _logger.LogInformation("Successfully updated a User, redirecting to Index");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating a User");
                    ViewBag.ErrorMessage = "An error occurred while updating a User. Please try again later.";
                }
                return RedirectToAction("Index", "OrdemServico");
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "An error occurred while parsing the form data.");
                TempData["ErrorMessage"] = "Houve um erro ao processar os dados fornecidos. Por favor, verifique se os dados estão corretos e tente novamente.";
                return View("Error"); // Retorna a view de erro
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var apiClient = _apiClientFactory.Create(_jwtToken);
                var ordemDeServico = await apiClient.GetAsync<ClienteOrdemServicoModelDTO>($"api/Cliente/{id}");

                if (ordemDeServico == null)
                {
                    _logger.LogWarning($"Tried to delete a User with ID {id} that does not exist.");
                    TempData["ErrorMessage"] = $"O usuário com ID {id} não existe.";
                    return View("Error"); // Retorna a view de erro
                }

                return View("Delete", ordemDeServico);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"An error occurred while trying to delete the User with ID {id}.");
                TempData["ErrorMessage"] = "Ocorreu um erro ao tentar excluir o usuário. Por favor, tente novamente mais tarde.";
                return View("Error"); // Retorna a view de erro
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var apiClient = _apiClientFactory.Create(_jwtToken);
                _logger.LogInformation("Sending a request to delete a User");
                await apiClient.DeleteAsync($"api/Cliente/{id}");

                _logger.LogInformation("Successfully deleted a User, redirecting to Index");
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"An error occurred while trying to delete the User with ID {id}.");
                TempData["ErrorMessage"] = "Ocorreu um erro ao tentar excluir o usuário. Por favor, tente novamente mais tarde.";
                return View("Error"); // Retorna a view de erro
            }
        }

    }
}
