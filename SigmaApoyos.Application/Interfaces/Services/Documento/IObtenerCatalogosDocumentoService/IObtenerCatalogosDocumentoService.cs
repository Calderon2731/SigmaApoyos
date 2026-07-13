using SigmaApoyos.Application.DTOs.Comunes;

namespace SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerCatalogosDocumentoService;

public interface IObtenerCatalogosDocumentoService
{
    Task<IReadOnlyList<OpcionDto>> ObtenerTiposDocumentoAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<OpcionDto>> ObtenerEstadosAsync(CancellationToken cancellationToken = default);
}
