using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {
        #region Methods
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {

                var user = new AppUser
                {
                    DisplayName = "Filan Fisteku",
                    Email = "admin@test.com",
                    UserName = "admin@test.com",
                    Users = new Users
                    {
                        FirstName = "Filan",
                        LastName = "Fisteku",
                    }
                };
                await userManager.CreateAsync(user, "Password123.*");
            };
        }
        #endregion
    }
}
