using SigmaApoyos.Application.DTOs.TiposDocumento;

namespace SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;

public interface IObtenerTiposDocumentoRepository
{
    Task<IReadOnlyList<TipoDocumentoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
