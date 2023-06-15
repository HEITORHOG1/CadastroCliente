using CadastroCliente.Web.Models;
using CadastroCliente.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CadastroCliente.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IApiClientFactory apiClientFactory, IConfiguration configuration, ILogger<LoginController> logger)
        {
            _apiClientFactory = apiClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogInformation("Received a request for Login view");
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("Received a request for Index view");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModelDTO model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Login attempt from user {Email}", model.Email);
                var apiClient = _apiClientFactory.Create("");
                var baseUrl = _configuration["BaseUrl"];
                var apiUrl = $"{baseUrl}Auth/login";
                var response = await apiClient.PostAsync(apiUrl, model);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    content = content.Trim();
                    var jwtToken = content;
                    HttpContext.Session.SetString("JwtToken", jwtToken);
                    _logger.LogInformation("Successfully logged in user {Email}", model.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogWarning("Failed to log in user {Email}", model.Email);
                    ModelState.AddModelError("", "Email or password is incorrect");
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Cleanup JWT token
            _logger.LogInformation("User is logging out");
            HttpContext.Session.Remove("JwtToken");
            // Redirect to the home page
            return RedirectToAction("Index", "Home");
        }
    }
}
