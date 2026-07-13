using Microsoft.AspNetCore.Identity;
using SigmaApoyos.Application.DTOs.Usuarios;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IActualizarUsuarioService;

namespace SigmaApoyos.Infrastructure.Identity.Services;

public class ActualizarUsuarioService : IActualizarUsuarioService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ActualizarUsuarioService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> ActualizarAsync(UpdateUsuarioDto dto, string usuarioActualId, CancellationToken cancellationToken = default)
    {
        var usuario = await _userManager.FindByIdAsync(dto.Id);

        if (usuario == null)
        {
            return false;
        }

        if (!await _roleManager.RoleExistsAsync(dto.RoleName))
        {
            return false;
        }

        var rolesActuales = await _userManager.GetRolesAsync(usuario);
        var esMismoUsuario = string.Equals(usuario.Id, usuarioActualId, StringComparison.Ordinal);
        var seQuiereDesactivar = dto.IdEstado == 1;
        var seQuiereQuitarAdmin = !IdentityRoles.EsAdministradorTotal(dto.RoleName);

        if (esMismoUsuario && (seQuiereDesactivar || seQuiereQuitarAdmin))
        {
            return false;
        }

        usuario.Nombre = dto.Nombre;
        usuario.PrimerApellido = dto.PrimerApellido;
        usuario.SegundoApellido = dto.SegundoApellido;
        usuario.Email = dto.Email;
        usuario.UserName = dto.Email;
        usuario.NormalizedEmail = _userManager.NormalizeEmail(dto.Email);
        usuario.NormalizedUserName = _userManager.NormalizeName(dto.Email);
        usuario.IdEstado = dto.IdEstado;

        var updateResult = await _userManager.UpdateAsync(usuario);

        if (!updateResult.Succeeded)
        {
            return false;
        }

        if (rolesActuales.Any())
        {
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(usuario, rolesActuales);

            if (!removeRolesResult.Succeeded)
            {
                return false;
            }
        }

        var addRoleResult = await _userManager.AddToRoleAsync(usuario, dto.RoleName);
        return addRoleResult.Succeeded;
    }
}
