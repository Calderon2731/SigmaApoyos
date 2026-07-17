using SigmaApoyos.Application.DTOs.Auditorias;
using SigmaApoyos.Application.DTOs.Comunes;

namespace SigmaApoyos.Application.Interfaces.Repositories.Auditorias;

public interface IObtenerAuditoriasRepository
{
    Task<ResultadoPaginadoDto<AuditoriaDto>> ObtenerTodosAsync(
        FiltroAuditoriaDto filtro,
        CancellationToken cancellationToken = default);
}
