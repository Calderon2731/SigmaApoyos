using SigmaApoyos.Application.DTOs.Auditorias;

namespace SigmaApoyos.Application.Interfaces.Repositories.Auditorias;

public interface IObtenerAuditoriaPorIdRepository
{
    Task<AuditoriaDto?> ObtenerPorIdAsync(long idAuditoria, CancellationToken cancellationToken = default);
}
