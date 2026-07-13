using SigmaApoyos.Application.DTOs.Comunes;

namespace SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerCatalogosUsuarioService;

public interface IObtenerCatalogosUsuarioService
{
    Task<IReadOnlyList<string>> ObtenerRolesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<OpcionDto>> ObtenerEstadosAsync(CancellationToken cancellationToken = default);
}
