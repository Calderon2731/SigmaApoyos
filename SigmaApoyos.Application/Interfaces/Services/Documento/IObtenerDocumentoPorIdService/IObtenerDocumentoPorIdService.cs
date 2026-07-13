using SigmaApoyos.Application.DTOs.Documentos;

namespace SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerDocumentoPorIdService;

public interface IObtenerDocumentoPorIdService
{
    Task<DocumentoDto?> ObtenerPorIdAsync(int idDocumento, CancellationToken cancellationToken = default);
}
