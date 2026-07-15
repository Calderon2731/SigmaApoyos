using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Interfaces.Repositories.Estados;

public interface ICrearEstadoRepository
{
    Task CrearAsync(Estado estado, CancellationToken cancellationToken = default);
}
