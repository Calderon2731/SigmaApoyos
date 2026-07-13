using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Usuarios;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuariosService;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Identity.Services;

public class ObtenerUsuariosService : IObtenerUsuariosService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public ObtenerUsuariosService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IReadOnlyList<UsuarioDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        var usuarios = await _userManager.Users
            .AsNoTracking()
            .OrderBy(x => x.Nombre)
            .ThenBy(x => x.PrimerApellido)
            .ToListAsync(cancellationToken);

        var estados = await _context.Estados
            .AsNoTracking()
            .ToDictionaryAsync(x => x.IdEstado, x => x.Nombre, cancellationToken);

        var resultado = new List<UsuarioDto>();

        foreach (var usuario in usuarios)
        {
            var roles = await _userManager.GetRolesAsync(usuario);

            resultado.Add(new UsuarioDto
            {
                Id = usuario.Id,
                NombreCompleto = $"{usuario.Nombre} {usuario.PrimerApellido} {usuario.SegundoApellido}".Trim(),
                Email = usuario.Email ?? string.Empty,
                Rol = roles.FirstOrDefault() ?? string.Empty,
                IdEstado = usuario.IdEstado,
                Estado = estados.TryGetValue(usuario.IdEstado, out var estado) ? estado : "No definido",
                FechaCreacion = usuario.FechaCreacion
            });
        }

        return resultado;
    }
}
