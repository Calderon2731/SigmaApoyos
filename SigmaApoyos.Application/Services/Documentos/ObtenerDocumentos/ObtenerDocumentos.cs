using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerDocumentoService;

namespace SigmaApoyos.Application.Services.Documentos.ObtenerDocumentos;

public sealed class ObtenerDocumentos : IObtenerDocumentoService
{
    private readonly IObtenerDocumentosRepository _obtenerDocumentosRepository;

    public ObtenerDocumentos(IObtenerDocumentosRepository obtenerDocumentosRepository)
    {
        _obtenerDocumentosRepository = obtenerDocumentosRepository;
    }

    public async Task<ResultadoPaginadoDto<DocumentoDto>> ObtenerTodosAsync(
        FiltroDocumentoDto filtro,
        CancellationToken cancellationToken = default)
    {
        return await _obtenerDocumentosRepository.ObtenerTodosAsync(filtro, cancellationToken);
    }
}
