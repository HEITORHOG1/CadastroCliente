using AutoMapper;
using CadastroCliente.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroCliente.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly IMapper _mapper;

        public UserController(ApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchString = null)
        {
            List<UserDTO> users;

            if (string.IsNullOrEmpty(searchString))
            {
                users = await _apiClient.GetAsync<List<UserDTO>>("api/User");
            }
            else
            {
                searchString = System.Net.WebUtility.UrlEncode(searchString);
                users = await _apiClient.GetAsync<List<UserDTO>>($"api/User/search/{searchString}");
            }

            var userDtos = _mapper.Map<List<UserDTO>>(users);
            return View(userDtos);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userDto);
            }

            await _apiClient.PostAsync("api/User", userDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userDto = await _apiClient.GetAsync<UserDTO>($"api/User/{id}");
            if (userDto == null)
            {
                return NotFound();
            }

            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userDto);
            }

            await _apiClient.PutAsync($"api/User/{userDto.Id}", userDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var userDto = await _apiClient.GetAsync<UserDTO>($"api/User/{id}");
            if (userDto == null)
            {
                return NotFound();
            }

            return View(userDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userDto = await _apiClient.GetAsync<UserDTO>($"api/User/{id}");
            if (userDto == null)
            {
                return NotFound();
            }

            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiClient.DeleteAsync($"api/User/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}
