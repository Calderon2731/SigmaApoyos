using SigmaApoyos.Application.DTOs.TiposDocumento;

namespace SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IObtenerTipoDocumentoService;

public interface IObtenerTipoDocumentoService
{
    Task<IReadOnlyList<TipoDocumentoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
