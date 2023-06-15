using AutoMapper;
using CadastroCliente.Web.Models;
using Microsoft.AspNetCore.Mvc;
using CadastroCliente.Web.Services;
using System.Net;
using Microsoft.Extensions.Logging;

namespace CadastroCliente.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly string _jwtToken;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly ILogger<UserController> _logger;
        public UserController(IApiClientFactory apiClientFactory, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<UserController> logger)
        {
            _apiClientFactory = apiClientFactory;
            _mapper = mapper;
            _jwtToken = httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchString = null)
        {
            _logger.LogInformation("Received a request to view the User index");

            var apiClient = _apiClientFactory.Create(_jwtToken);

            List<UserDTO> users;

            try
            {
                if (string.IsNullOrEmpty(searchString))
                {
                    _logger.LogInformation("Requesting list of all users");
                    users = await apiClient.GetAsync<List<UserDTO>>("api/User");
                }
                else
                {
                    _logger.LogInformation("Requesting search for users with string: {SearchString}", searchString);
                    searchString = System.Net.WebUtility.UrlEncode(searchString);
                    users = await apiClient.GetAsync<List<UserDTO>>($"api/User/search/{searchString}");
                }
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogError("Unauthorized request");
                ModelState.AddModelError("", "Please log in to continue.");
                return RedirectToAction("Login", "Login");
            }

            var userDtos = _mapper.Map<List<UserDTO>>(users);
            return View(userDtos);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Received a request to create a new User");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Received an invalid User creation request");
                return View(userDto);
            }

            var apiClient = _apiClientFactory.Create(_jwtToken);
            _logger.LogInformation("Sending a request to create a new User");
            await apiClient.PostAsync("api/User", userDto);

            _logger.LogInformation("Successfully created a new User, redirecting to Index");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Received a request to edit User with id: {Id}", id);

            var apiClient = _apiClientFactory.Create(_jwtToken);

            var userDto = await apiClient.GetAsync<UserDTO>($"api/User/{id}");
            if (userDto == null)
            {
                _logger.LogWarning("User with id: {Id} not found", id);
                return NotFound();
            }

            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Received an invalid User edit request");
                return View(userDto);
            }

            var apiClient = _apiClientFactory.Create(_jwtToken);
            _logger.LogInformation("Sending a request to edit User with id: {Id}", userDto.Id);
            await apiClient.PutAsync($"api/User/{userDto.Id}", userDto);

            _logger.LogInformation("Successfully edited User with id: {Id}, redirecting to Index", userDto.Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Received a request to view details of User with id: {Id}", id);

            var apiClient = _apiClientFactory.Create(_jwtToken);

            var userDto = await apiClient.GetAsync<UserDTO>($"api/User/{id}");
            if (userDto == null)
            {
                _logger.LogWarning("User with id: {Id} not found", id);
                return NotFound();
            }

            return View(userDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Received a request to delete User with id: {Id}", id);

            var apiClient = _apiClientFactory.Create(_jwtToken);

            var userDto = await apiClient.GetAsync<UserDTO>($"api/User/{id}");
            if (userDto == null)
            {
                _logger.LogWarning("User with id: {Id} not found", id);
                return NotFound();
            }

            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("Received a confirmation to delete User with id: {Id}", id);

            var apiClient = _apiClientFactory.Create(_jwtToken);

            await apiClient.DeleteAsync($"api/User/{id}");

            _logger.LogInformation("Successfully deleted User with id: {Id}, redirecting to Index", id);
            return RedirectToAction(nameof(Index));
        }
    }
}
