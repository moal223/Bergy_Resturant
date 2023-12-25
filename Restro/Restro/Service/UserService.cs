using Microsoft.AspNetCore.Identity;
using Restro.Models;

namespace Restro.Service
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<IdentityUser>> GetAllUsersAsync()
        {
            return await Task.Run(() => _context.Users.ToList());
        }
    }
}
