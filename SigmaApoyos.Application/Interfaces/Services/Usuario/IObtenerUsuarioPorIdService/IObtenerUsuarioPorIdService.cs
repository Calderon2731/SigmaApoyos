using SigmaApoyos.Application.DTOs.Usuarios;

namespace SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuarioPorIdService;

public interface IObtenerUsuarioPorIdService
{
    Task<UpdateUsuarioDto?> ObtenerParaEditarAsync(string idUsuario, CancellationToken cancellationToken = default);
}
