using Microsoft.AspNetCore.Identity;
using Restro.Models;

namespace Restro.Service
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;
        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<IdentityRole>> GetAllRolesAsync()
        {
            return await Task.Run(() => _context.Roles.ToList());
        }
    }
}
