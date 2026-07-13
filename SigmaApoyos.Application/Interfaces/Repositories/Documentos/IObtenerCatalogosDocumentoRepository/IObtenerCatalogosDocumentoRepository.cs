using SigmaApoyos.Application.DTOs.Comunes;

namespace SigmaApoyos.Application.Interfaces.Repositories.Documentos;

public interface IObtenerCatalogosDocumentoRepository
{
    Task<IReadOnlyList<OpcionDto>> ObtenerTiposDocumentoAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<OpcionDto>> ObtenerEstadosAsync(CancellationToken cancellationToken = default);
}
