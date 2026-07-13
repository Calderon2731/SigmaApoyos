using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerCatalogosUsuarioService;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Identity.Services;

public class ObtenerCatalogosUsuarioService : IObtenerCatalogosUsuarioService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public ObtenerCatalogosUsuarioService(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<IReadOnlyList<string>> ObtenerRolesAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _roleManager.Roles
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => x.Name ?? string.Empty)
            .ToListAsync(cancellationToken);

        return roles
            .Where(IdentityRoles.EsRolVisible)
            .ToList();
    }

    public async Task<IReadOnlyList<OpcionDto>> ObtenerEstadosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Estados
            .AsNoTracking()
            .OrderBy(x => x.Nombre)
            .Select(x => new OpcionDto
            {
                Id = x.IdEstado,
                Nombre = x.Nombre
            })
            .ToListAsync(cancellationToken);
    }
}
