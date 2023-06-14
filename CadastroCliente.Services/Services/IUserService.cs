using CadastroCliente.Model;

namespace CadastroCliente.Services.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<User>> SearchUsersAsync(string searchString);
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
