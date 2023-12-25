using Microsoft.AspNetCore.Identity;

namespace Restro.Service
{
    public interface IRoleService
    {
        Task<IList<IdentityRole>> GetAllRolesAsync();
    }
}
