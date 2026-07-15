using SigmaApoyos.Application.DTOs.Auditorias;

namespace SigmaApoyos.Application.Interfaces.Services.Auditoria.IObtenerAuditoriaPorIdService;

public interface IObtenerAuditoriaPorIdService
{
    Task<AuditoriaDto?> ObtenerPorIdAsync(long idAuditoria, CancellationToken cancellationToken = default);
}
