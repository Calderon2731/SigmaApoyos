using SigmaApoyos.Application.DTOs.Usuarios;

namespace SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuariosService;

public interface IObtenerUsuariosService
{
    Task<IReadOnlyList<UsuarioDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
