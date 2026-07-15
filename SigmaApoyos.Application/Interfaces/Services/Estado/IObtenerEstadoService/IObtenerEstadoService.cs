using SigmaApoyos.Application.DTOs.Estados;

namespace SigmaApoyos.Application.Interfaces.Services.Estado.IObtenerEstadoService;

public interface IObtenerEstadoService
{
    Task<IReadOnlyList<EstadoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
