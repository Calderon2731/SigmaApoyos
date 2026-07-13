using Microsoft.AspNetCore.Identity;

namespace SigmaApoyos.Infrastructure.Identity.Seed;

public static class IdentityRoleSeeder
{
    private static readonly string[] Roles =
    {
        IdentityRoles.Docente,
        IdentityRoles.Directora,
        IdentityRoles.Subdirector,
        IdentityRoles.Auxiliar,
        IdentityRoles.Biblioteca,
        IdentityRoles.Oficinistas,
        IdentityRoles.Guia,
        IdentityRoles.Orientacion,
        IdentityRoles.CoordinadorAcademico
    };

    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
