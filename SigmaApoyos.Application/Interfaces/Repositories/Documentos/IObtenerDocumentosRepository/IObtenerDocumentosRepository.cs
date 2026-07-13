using SigmaApoyos.Application.DTOs.Documentos;

namespace SigmaApoyos.Application.Interfaces.Repositories.Documentos;

public interface IObtenerDocumentosRepository
{
    Task<IReadOnlyList<DocumentoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
