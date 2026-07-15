using SigmaApoyos.Application.DTOs.TiposDocumento;

namespace SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IObtenerTipoDocumentoPorIdService;

public interface IObtenerTipoDocumentoPorIdService
{
    Task<TipoDocumentoDto?> ObtenerPorIdAsync(int idTipoDocumento, CancellationToken cancellationToken = default);
}
