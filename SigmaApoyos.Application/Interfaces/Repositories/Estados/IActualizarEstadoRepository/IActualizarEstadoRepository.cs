using SigmaApoyos.Application.DTOs.Estados;

namespace SigmaApoyos.Application.Interfaces.Repositories.Estados;

public interface IActualizarEstadoRepository
{
    Task ActualizarAsync(UpdateEstadoDto dto, CancellationToken cancellationToken = default);
}
