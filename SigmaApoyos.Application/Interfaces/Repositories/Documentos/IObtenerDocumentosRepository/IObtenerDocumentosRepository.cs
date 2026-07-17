using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.DTOs.Documentos;

namespace SigmaApoyos.Application.Interfaces.Repositories.Documentos;

public interface IObtenerDocumentosRepository
{
    Task<ResultadoPaginadoDto<DocumentoDto>> ObtenerTodosAsync(
        FiltroDocumentoDto filtro,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DocumentoDto>> ObtenerPorExpedienteAsync(
        string identificacionEstudiante,
        CancellationToken cancellationToken = default);
}
