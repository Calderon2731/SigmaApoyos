using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerCatalogosDocumentoService;

namespace SigmaApoyos.Application.Services.Documentos.ObtenerCatalogosDocumento;

public sealed class ObtenerCatalogosDocumento : IObtenerCatalogosDocumentoService
{
    private readonly IObtenerCatalogosDocumentoRepository _obtenerCatalogosDocumentoRepository;

    public ObtenerCatalogosDocumento(IObtenerCatalogosDocumentoRepository obtenerCatalogosDocumentoRepository)
    {
        _obtenerCatalogosDocumentoRepository = obtenerCatalogosDocumentoRepository;
    }

    public async Task<IReadOnlyList<OpcionDto>> ObtenerTiposDocumentoAsync(CancellationToken cancellationToken = default)
    {
        return await _obtenerCatalogosDocumentoRepository.ObtenerTiposDocumentoAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<OpcionDto>> ObtenerEstadosAsync(CancellationToken cancellationToken = default)
    {
        return await _obtenerCatalogosDocumentoRepository.ObtenerEstadosAsync(cancellationToken);
    }
}
