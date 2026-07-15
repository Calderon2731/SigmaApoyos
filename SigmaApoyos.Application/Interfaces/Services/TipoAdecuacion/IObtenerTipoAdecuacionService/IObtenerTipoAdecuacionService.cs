using SigmaApoyos.Application.DTOs.TiposAdecuacion;

namespace SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IObtenerTipoAdecuacionService;

public interface IObtenerTipoAdecuacionService
{
    Task<IReadOnlyList<TipoAdecuacionDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
