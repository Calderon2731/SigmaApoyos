using SigmaApoyos.Application.DTOs.Auditorias;

namespace SigmaApoyos.Application.Interfaces.Repositories.Auditorias;

public interface IObtenerAuditoriasRepository
{
    Task<IReadOnlyList<AuditoriaDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
