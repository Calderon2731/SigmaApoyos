using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IEliminarExpedienteRepository;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IEliminarExpedienteService;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Services.Expedientes.EliminarExpediente;

public sealed class EliminarExpediente : IEliminarExpedienteService
{
    private readonly IEliminarExpedienteRepository _eliminarExpedienteRepository;
    private readonly IObtenerExpedientePorIdRepository _obtenerExpedientePorIdRepository;

    public EliminarExpediente(
        IEliminarExpedienteRepository eliminarExpedienteRepository,
        IObtenerExpedientePorIdRepository obtenerExpedientePorIdRepository)
    {
        _eliminarExpedienteRepository = eliminarExpedienteRepository;
        _obtenerExpedientePorIdRepository = obtenerExpedientePorIdRepository;
    }

    public async Task<bool> EliminarAsync(string identificacionEstudiante, CancellationToken cancellationToken = default)
    {
        var expedienteExistente = await _obtenerExpedientePorIdRepository.ObtenerPorIdAsync(identificacionEstudiante, cancellationToken);

        if (expedienteExistente == null)
        {
            return false;
        }

        var expediente = new Expediente
        {
            IdentificacionEstudiante = expedienteExistente.IdentificacionEstudiante
        };

        await _eliminarExpedienteRepository.EliminarAsync(expediente, cancellationToken);
        return true;
    }
}
