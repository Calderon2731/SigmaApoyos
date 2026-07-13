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

    public async Task<IReadOnlyList<ExpedienteDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _obtenerExpedientesRepository.ObtenerTodosAsync(cancellationToken);
    }
}

