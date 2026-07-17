using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.DTOs.Usuarios;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuariosService;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Identity.Services;

public class ObtenerUsuariosService : IObtenerUsuariosService
{
    private const int RegistrosPorPagina = 10;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public ObtenerUsuariosService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<ResultadoPaginadoDto<UsuarioDto>> ObtenerTodosAsync(
        FiltroUsuarioDto filtro,
        CancellationToken cancellationToken = default)
    {
        var consulta = _userManager.Users
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro.Nombre))
        {
            string nombre = filtro.Nombre.Trim().ToLower();
            consulta = consulta.Where(usuario =>
                usuario.Nombre.ToLower().Contains(nombre) ||
                usuario.PrimerApellido.ToLower().Contains(nombre) ||
                usuario.SegundoApellido.ToLower().Contains(nombre) ||
                (usuario.Nombre + " " + usuario.PrimerApellido + " " + usuario.SegundoApellido)
                    .ToLower()
                    .Contains(nombre));
        }

        if (filtro.IdEstado.HasValue)
        {
            consulta = consulta.Where(usuario => usuario.IdEstado == filtro.IdEstado.Value);
        }

        if (!string.IsNullOrWhiteSpace(filtro.Rol))
        {
            string rolNormalizado = filtro.Rol.Trim().ToUpperInvariant();
            string? rolId = await _context.Roles
                .AsNoTracking()
                .Where(rol => rol.NormalizedName == rolNormalizado)
                .Select(rol => rol.Id)
                .FirstOrDefaultAsync(cancellationToken);

            consulta = rolId == null
                ? consulta.Where(_ => false)
                : consulta.Where(usuario => _context.UserRoles.Any(usuarioRol =>
                    usuarioRol.UserId == usuario.Id && usuarioRol.RoleId == rolId));
        }

        int totalRegistros = await consulta.CountAsync(cancellationToken);
        int totalPaginas = Math.Max(1, (int)Math.Ceiling(totalRegistros / (double)RegistrosPorPagina));
        int paginaActual = Math.Clamp(filtro.Pagina, 1, totalPaginas);

        var usuarios = await consulta
            .OrderBy(usuario => usuario.Nombre)
            .ThenBy(usuario => usuario.PrimerApellido)
            .Skip((paginaActual - 1) * RegistrosPorPagina)
            .Take(RegistrosPorPagina)
            .ToListAsync(cancellationToken);

        var estados = await _context.Estados
            .AsNoTracking()
            .ToDictionaryAsync(estado => estado.IdEstado, estado => estado.Nombre, cancellationToken);

        var usuariosIds = usuarios.Select(usuario => usuario.Id).ToList();
        var rolesUsuarios = await (
            from usuarioRol in _context.UserRoles.AsNoTracking()
            join rol in _context.Roles.AsNoTracking() on usuarioRol.RoleId equals rol.Id
            where usuariosIds.Contains(usuarioRol.UserId)
            select new { usuarioRol.UserId, Nombre = rol.Name ?? string.Empty })
            .ToListAsync(cancellationToken);

        var rolPorUsuario = rolesUsuarios
            .GroupBy(x => x.UserId)
            .ToDictionary(grupo => grupo.Key, grupo => grupo.First().Nombre);

        var resultado = usuarios.Select(usuario => new UsuarioDto
        {
            Id = usuario.Id,
            NombreCompleto = $"{usuario.Nombre} {usuario.PrimerApellido} {usuario.SegundoApellido}".Trim(),
            Email = usuario.Email ?? string.Empty,
            Rol = rolPorUsuario.TryGetValue(usuario.Id, out var rol) ? rol : string.Empty,
            IdEstado = usuario.IdEstado,
            Estado = estados.TryGetValue(usuario.IdEstado, out var estado) ? estado : "No definido",
            FechaCreacion = usuario.FechaCreacion
        }).ToList();

        return new ResultadoPaginadoDto<UsuarioDto>
        {
            Registros = resultado,
            PaginaActual = paginaActual,
            TotalPaginas = totalPaginas,
            TotalRegistros = totalRegistros
        };
    }
}
