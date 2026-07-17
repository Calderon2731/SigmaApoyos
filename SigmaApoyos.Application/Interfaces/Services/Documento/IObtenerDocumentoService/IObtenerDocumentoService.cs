using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.DTOs.Documentos;

namespace SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerDocumentoService;

public interface IObtenerDocumentoService
{
    Task<ResultadoPaginadoDto<DocumentoDto>> ObtenerTodosAsync(
        FiltroDocumentoDto filtro,
        CancellationToken cancellationToken = default);
}
