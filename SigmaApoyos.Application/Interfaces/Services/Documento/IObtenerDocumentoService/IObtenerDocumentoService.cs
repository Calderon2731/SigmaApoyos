using SigmaApoyos.Application.DTOs.Documentos;

namespace SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerDocumentoService;

public interface IObtenerDocumentoService
{
    Task<IReadOnlyList<DocumentoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
