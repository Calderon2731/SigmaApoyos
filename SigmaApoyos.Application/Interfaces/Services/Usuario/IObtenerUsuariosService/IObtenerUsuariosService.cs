using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.DTOs.Usuarios;

namespace SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuariosService;

public interface IObtenerUsuariosService
{
    Task<ResultadoPaginadoDto<UsuarioDto>> ObtenerTodosAsync(
        FiltroUsuarioDto filtro,
        CancellationToken cancellationToken = default);
}
