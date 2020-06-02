using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager) 
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Normal",
                    Email = "normal@test.com",
                    UserName = "normal@test.com",
                    IsPayroll = true,
                    Address = new Address
                    {
                        FirstName = "Normal",
                        LastName = "Client",
                        Building = "ICT",
                        Room = "2A",
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}