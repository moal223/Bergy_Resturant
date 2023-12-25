using Microsoft.AspNetCore.Identity;

namespace Restro.Service
{
    public interface IUserService
    {
        Task<IList<IdentityUser>> GetAllUsersAsync();
    }
}
