using SigmaApoyos.Application.DTOs.TiposAdecuacion;

namespace SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;

public interface IObtenerTiposAdecuacionRepository
{
    Task<IReadOnlyList<TipoAdecuacionDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
