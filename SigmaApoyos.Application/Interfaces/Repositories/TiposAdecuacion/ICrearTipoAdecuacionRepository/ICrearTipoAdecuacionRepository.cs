using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;

public interface ICrearTipoAdecuacionRepository
{
    Task CrearAsync(TipoAdecuacion tipoAdecuacion, CancellationToken cancellationToken = default);
}
