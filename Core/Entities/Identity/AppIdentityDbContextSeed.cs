using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
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
                    },
                    new AppUser
                    {
                        DisplayName = "Admin",
                        Email = "admin@test.com",
                        UserName = "admin@test.com",
                        IsPayroll = true
                    },
                    new AppUser
                    {
                        DisplayName = "NY Pizza",
                        Email = "nypizza@test.com",
                        UserName = "nypizza@test.com",
                        IsPayroll = true
                    },
                    new AppUser
                    {
                        DisplayName = "Burger King",
                        Email = "bk@test.com",
                        UserName = "bk@test.com",
                        IsPayroll = true
                    },
                    new AppUser
                    {
                        DisplayName = "McDonalds",
                        Email = "mcdonalds@test.com",
                        UserName = "mcdonalds@test.com",
                        IsPayroll = true
                    }
                };

                var roles = new List<AppRole>
                {
                    new AppRole {Name = "Admin"},
                    new AppRole {Name = "Manager"},
                    new AppRole {Name = "Member"}
                };

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Ny Pizza"),
                    new Claim(ClaimTypes.Name, "Burger King"),
                    new Claim(ClaimTypes.Name, "McDonalds"),
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(user, "Member");
                    if (user.Email == "admin@test.com") await userManager.AddToRoleAsync(user, "Admin");
                    if (user.Email == "admin@test.com") await userManager.AddToRoleAsync(user, "Manager");
                    if (user.Email == "nypizza@test.com") await userManager.AddToRoleAsync(user, "Manager");
                    if (user.Email == "bk@test.com") await userManager.AddToRoleAsync(user, "Manager");
                    if (user.Email == "mcdonalds@test.com") await userManager.AddToRoleAsync(user, "Manager");

                    if (user.Email == "nypizza@test.com") await userManager.AddClaimAsync(user, claims.FirstOrDefault(x => x.Value == "Ny Pizza"));
                    if (user.Email == "bk@test.com") await userManager.AddClaimAsync(user, claims.FirstOrDefault(x => x.Value == "Burger King"));
                    if (user.Email == "mcdonalds@test.com") await userManager.AddClaimAsync(user, claims.FirstOrDefault(x => x.Value == "McDonalds"));
                }
            }
        }
    }
}