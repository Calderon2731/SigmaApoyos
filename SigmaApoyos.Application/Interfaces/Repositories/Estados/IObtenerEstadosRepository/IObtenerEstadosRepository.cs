using SigmaApoyos.Application.DTOs.Estados;

namespace SigmaApoyos.Application.Interfaces.Repositories.Estados;

public interface IObtenerEstadosRepository
{
    Task<IReadOnlyList<EstadoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
