using SigmaApoyos.Application.DTOs.Documentos;

namespace SigmaApoyos.Application.Interfaces.Repositories.Documentos;

public interface IObtenerDocumentoPorIdRepository
{
    Task<DocumentoDto?> ObtenerPorIdAsync(int idDocumento, CancellationToken cancellationToken = default);
}
