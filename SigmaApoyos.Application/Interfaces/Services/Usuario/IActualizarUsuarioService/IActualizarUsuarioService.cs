using SigmaApoyos.Application.DTOs.Usuarios;

namespace SigmaApoyos.Application.Interfaces.Services.Usuario.IActualizarUsuarioService;

public interface IActualizarUsuarioService
{
    Task<bool> ActualizarAsync(UpdateUsuarioDto dto, string usuarioActualId, CancellationToken cancellationToken = default);
}
