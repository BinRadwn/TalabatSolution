using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager , ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            var UserWithAddress = await userManager.Users.Include(U=>U.Address).SingleOrDefaultAsync(E=>E.Email==email);

            return UserWithAddress;
        }

    }
}
