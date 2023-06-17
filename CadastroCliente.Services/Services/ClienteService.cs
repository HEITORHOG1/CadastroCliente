using CadastroCliente.Infra.IRepository;
using CadastroCliente.Model;
using CadastroCliente.Services.Validators;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CadastroCliente.Services.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<UserService> _logger;
        private readonly ClienteOrdemServicoModelValidator _validator;

        public ClienteService(IClienteRepository clienteRepository, ILogger<UserService> logger, ClienteOrdemServicoModelValidator validator)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ClienteOrdemServicoModel> CreateUserAsync(ClienteOrdemServicoModel user)
        {
            var validationResult = _validator.Validate(user);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation failed.");
                foreach (var error in validationResult.Errors)
                {
                    _logger.LogError($"Property: {error.PropertyName} Error: {error.ErrorMessage}");
                }
                throw new ValidationException("Validation failed.", validationResult.Errors);
            }

            return await _clienteRepository.CreateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid id provided for deletion.");
                throw new ArgumentException("Invalid id provided for deletion.");
            }

            await _clienteRepository.DeleteUserAsync(id);
        }


        public async Task<ClienteOrdemServicoModel> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Invalid id provided for retrieval.");
                throw new ArgumentException("Invalid id provided for retrieval.");
            }

            return await _clienteRepository.GetUserByIdAsync(id);
        }


        public async Task<IEnumerable<ClienteOrdemServicoModel>> GetUsersAsync()
        {
            try
            {
                _logger.LogInformation("Getting all users.");
                return await _clienteRepository.GetUsersAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while getting all users.");
                throw;
            }
        }


        public async Task<IEnumerable<ClienteOrdemServicoModel>> GetUsersAsync(string search = null)
        {
            var validator = new SearchValidator();
            var validationResult = validator.Validate(search);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _clienteRepository.GetUsersAsync(search);
        }
      

        public async Task<ClienteOrdemServicoModel> UpdateUserAsync(ClienteOrdemServicoModel user)
        {
            var validationResult = _validator.Validate(user);
            if (!validationResult.IsValid)
            {
                  _logger.LogError("Validation failed.");
                foreach (var error in validationResult.Errors)
                {
                    _logger.LogError($"Property: {error.PropertyName} Error: {error.ErrorMessage}");
                }
                throw new ValidationException("Validation failed.", validationResult.Errors);
            }
            return await _clienteRepository.UpdateUserAsync(user);
        }
    }
}
