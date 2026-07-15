using SigmaApoyos.Application.DTOs.TiposAdecuacion;

namespace SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;

public interface IActualizarTipoAdecuacionRepository
{
    Task ActualizarAsync(UpdateTipoAdecuacionDto dto, CancellationToken cancellationToken = default);
}
