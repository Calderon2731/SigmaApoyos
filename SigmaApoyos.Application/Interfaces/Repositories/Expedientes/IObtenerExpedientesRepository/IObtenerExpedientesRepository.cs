using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.DTOs.Expedientes;

namespace SigmaApoyos.Application.Interfaces.Repositories.Expedientes;

public interface IObtenerExpedientesRepository
{
    Task<ResultadoPaginadoDto<ExpedienteDto>> ObtenerTodosAsync(
        FiltroExpedienteDto filtro,
        CancellationToken cancellationToken = default);
}
