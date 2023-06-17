using Microsoft.AspNetCore.Mvc;
using CadastroCliente.Model;

using CadastroCliente.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;

namespace CadastroCliente.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // Adicione esta anotação para exigir autenticação com o esquema Bearer JWT
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IOrdemServicoService _ordemServicoRepository;
        public ClienteController(IClienteService clienteService, IOrdemServicoService ordemServicoRepository)
        {
            _clienteService = clienteService;
            _ordemServicoRepository = ordemServicoRepository;
        }

        [HttpGet("Buscar")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes([FromQuery] string termoBusca)
        {
            try
            {
                var clientes = await _clienteService.GetUsersAsync(termoBusca);
                return Ok(clientes);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                var errorResponse = new
                {
                    Message = "Erros de validação ocorreram",
                    Errors = errors
                };

                return BadRequest(errorResponse);
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                var clientes = await _clienteService.GetUsersAsync();
                return Ok(clientes);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                var errorResponse = new
                {
                    Message = "Erros de validação ocorreram",
                    Errors = errors
                };

                return BadRequest(errorResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id)
        {
            try
            {
                var clientes = await _clienteService.GetUsersAsync();
                var cliente = await _clienteService.GetUserByIdAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                return Ok(cliente);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                var errorResponse = new
                {
                    Message = "Erros de validação ocorreram",
                    Errors = errors
                };

                return BadRequest(errorResponse);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClienteOrdemServicoModel>> CreateUserAsync(ClienteOrdemServicoModel model)
        {
            try
            {
                var clientes = await _clienteService.GetUsersAsync();
                var createdModel = await _clienteService.CreateUserAsync(model);

                if (createdModel == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(GetClienteById), new { id = createdModel.Cliente.Id }, createdModel);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                var errorResponse = new
                {
                    Message = "Erros de validação ocorreram",
                    Errors = errors
                };

                return BadRequest(errorResponse);
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteOrdemServicoModel>> UpdateCliente(int id, ClienteOrdemServicoModel cliente)
        {
            try
            {
                var clientes = await _clienteService.GetUsersAsync();
                var updatedCliente = await _clienteService.UpdateUserAsync(cliente);
                if (updatedCliente == null)
                {
                    return NotFound();
                }

                return Ok(updatedCliente);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                var errorResponse = new
                {
                    Message = "Erros de validação ocorreram",
                    Errors = errors
                };

                return BadRequest(errorResponse);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                var clientes = await _clienteService.GetUsersAsync();
                var cliente = await _clienteService.GetUserByIdAsync(id);

                if (cliente == null)
                {
                    return NotFound();
                }

                await _clienteService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                var errorResponse = new
                {
                    Message = "Erros de validação ocorreram",
                    Errors = errors
                };

                return BadRequest(errorResponse);
            }
        }

    }
}

