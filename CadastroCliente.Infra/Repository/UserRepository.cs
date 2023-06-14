using CadastroCliente.Infra.Contexts;
using CadastroCliente.Infra.IRepository;
using CadastroCliente.Model;
using Microsoft.EntityFrameworkCore;

namespace CadastroCliente.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchString)
        {
            return await _context.Users
                .Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString))
                .ToListAsync();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null) return null;

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();

            return existingUser;
        }
    }
}
