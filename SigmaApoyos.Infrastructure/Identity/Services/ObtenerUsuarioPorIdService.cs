using Microsoft.AspNetCore.Identity;
using SigmaApoyos.Application.DTOs.Usuarios;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuarioPorIdService;

namespace SigmaApoyos.Infrastructure.Identity.Services;

public class ObtenerUsuarioPorIdService : IObtenerUsuarioPorIdService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ObtenerUsuarioPorIdService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UpdateUsuarioDto?> ObtenerParaEditarAsync(string idUsuario, CancellationToken cancellationToken = default)
    {
        var usuario = await _userManager.FindByIdAsync(idUsuario);

        if (usuario == null)
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(usuario);

        return new UpdateUsuarioDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            PrimerApellido = usuario.PrimerApellido,
            SegundoApellido = usuario.SegundoApellido,
            Email = usuario.Email ?? string.Empty,
            RoleName = roles.FirstOrDefault() ?? string.Empty,
            IdEstado = usuario.IdEstado
        };
    }
}
