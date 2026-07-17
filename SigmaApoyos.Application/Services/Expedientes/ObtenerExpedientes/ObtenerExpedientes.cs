using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedienteService;

namespace SigmaApoyos.Application.Services.Expedientes.ObtenerExpedientes;

public sealed class ObtenerExpedientes : IObtenerExpedienteService
{
    private readonly IObtenerExpedientesRepository _obtenerExpedientesRepository;

    public ObtenerExpedientes(IObtenerExpedientesRepository obtenerExpedientesRepository)
    {
        _obtenerExpedientesRepository = obtenerExpedientesRepository;
    }

    public async Task<ResultadoPaginadoDto<ExpedienteDto>> ObtenerTodosAsync(
        FiltroExpedienteDto filtro,
        CancellationToken cancellationToken = default)
    {
        return await _obtenerExpedientesRepository.ObtenerTodosAsync(filtro, cancellationToken);
    }
}
