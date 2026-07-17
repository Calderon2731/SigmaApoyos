using SigmaApoyos.Application.DTOs.Auditorias;
using SigmaApoyos.Application.DTOs.Comunes;

namespace SigmaApoyos.Application.Interfaces.Services.Auditoria.IObtenerAuditoriasService;

public interface IObtenerAuditoriasService
{
    Task<ResultadoPaginadoDto<AuditoriaDto>> ObtenerTodosAsync(
        FiltroAuditoriaDto filtro,
        CancellationToken cancellationToken = default);
}
