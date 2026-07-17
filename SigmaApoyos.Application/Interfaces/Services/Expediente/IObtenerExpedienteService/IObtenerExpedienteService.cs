using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.DTOs.Expedientes;

namespace SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedienteService;

public interface IObtenerExpedienteService
{
    Task<ResultadoPaginadoDto<ExpedienteDto>> ObtenerTodosAsync(
        FiltroExpedienteDto filtro,
        CancellationToken cancellationToken = default);
}
