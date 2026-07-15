using SigmaApoyos.Application.DTOs.Auditorias;

namespace SigmaApoyos.Application.Interfaces.Services.Auditoria.IObtenerAuditoriasService;

public interface IObtenerAuditoriasService
{
    Task<IReadOnlyList<AuditoriaDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
