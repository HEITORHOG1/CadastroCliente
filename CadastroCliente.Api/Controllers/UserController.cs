using CadastroCliente.Services.Services;
using CadastroCliente.Model;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace CadastroCliente.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // Adicione esta anotação para exigir autenticação com o esquema Bearer JWT
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("protected")]
        public IActionResult ProtectedMethod()
        {
            // A autenticação foi bem-sucedida, o token é válido
            // Você pode acessar informações do usuário autenticado através do objeto User
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Resto do código do método protegido
            return Ok();
        }
        /// <summary>
        /// Pesquisa usuários baseado em um critério de pesquisa
        /// </summary>
        /// <param name="query">Cadeia de caracteres a ser pesquisada nos usuários</param>
        /// <returns>Retorna a lista de usuários que correspondem ao critério de pesquisa</returns>
        [HttpGet("search/{query}")]
        public async Task<IActionResult> SearchUsers(string query)
        {
            try
            {
                var users = await _userService.SearchUsersAsync(query);
                if (users == null || !users.Any())
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtém a lista de todos os usuários
        /// </summary>
        /// <returns>Retorna uma lista de todos os usuários</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        /// <summary>
        /// Obtém um usuário específico pelo seu ID
        /// </summary>
        /// <param name="id">O ID do usuário</param>
        /// <returns>Retorna o usuário correspondente ao ID fornecido</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="user">O objeto User a ser criado</param>
        /// <returns>Retorna o usuário criado</returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
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


        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="id">O ID do usuário a ser atualizado</param>
        /// <param name="user">O objeto User atualizado</param>
        /// <returns>Retorna status NoContent se a atualização foi bem sucedida</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest(new { Message = "Os IDs fornecidos não são iguais." });
            }

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(user);

                if (updatedUser == null)
                {
                    return NotFound(new { Message = "Usuário não encontrado." });
                }

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

        /// <summary>
        /// Deleta um usuário existente
        /// </summary>
        /// <param name="id">O ID do usuário a ser deletado</param>
        /// <returns>Retorna status NoContent se a deleção foi bem sucedida</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
