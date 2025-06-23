using EventMaster.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace EventMaster.Infrastructure.Context;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        RoleManager<IdentityRole> roleManager)
    {
        await SeedRolesAsync(roleManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = [UserRoles.Admin, UserRoles.EventOrganizer, UserRoles.Participant];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
