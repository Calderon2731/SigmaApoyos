using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Interfaces.Repositories.Estados;

public interface IObtenerEstadoPorIdRepository
{
    Task<Estado?> ObtenerPorIdAsync(int idEstado, CancellationToken cancellationToken = default);
}
