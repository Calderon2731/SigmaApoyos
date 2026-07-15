using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;

public interface IObtenerTipoAdecuacionPorIdRepository
{
    Task<TipoAdecuacion?> ObtenerPorIdAsync(int idTipoAdecuacion, CancellationToken cancellationToken = default);
}
