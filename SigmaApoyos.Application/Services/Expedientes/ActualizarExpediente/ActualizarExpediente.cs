using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IActualizarExpedienteRepository;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IActualizarExpedienteService;

namespace SigmaApoyos.Application.Services.Expedientes.ActualizarExpediente;

public sealed class ActualizarExpediente : IActualizarExpedienteService
{
    private readonly IActualizarExpedienteRepository _actualizarExpedienteRepository;
    private readonly IObtenerExpedientePorIdRepository _obtenerExpedientePorIdRepository;

    public ActualizarExpediente(
        IActualizarExpedienteRepository actualizarExpedienteRepository,
        IObtenerExpedientePorIdRepository obtenerExpedientePorIdRepository)
    {
        _actualizarExpedienteRepository = actualizarExpedienteRepository;
        _obtenerExpedientePorIdRepository = obtenerExpedientePorIdRepository;
    }

    public async Task<UpdateExpedienteDto?> ObtenerParaEditarAsync(string identificacion, CancellationToken cancellationToken = default)
    {
        var expediente = await _obtenerExpedientePorIdRepository.ObtenerPorIdAsync(identificacion, cancellationToken);

        if (expediente == null)
        {
            return null;
        }

        return new UpdateExpedienteDto
        {
            IdentificacionEstudiante = expediente.IdentificacionEstudiante,
            Nombre = expediente.Nombre,
            PrimerApellido = expediente.PrimerApellido,
            SegundoApellido = expediente.SegundoApellido,
            FechaNacimiento = DateOnly.FromDateTime(expediente.FechaNacimiento),
            NombreEncargado = expediente.NombreEncargado,
            TelefonoEncargado = expediente.TelefonoEncargado,
            Observaciones = expediente.Observaciones,
            IdTipoAdecuacion = expediente.IdTipoAdecuacion,
            IdEstado = expediente.IdEstado
        };
    }

    public async Task<bool> ActualizarAsync(UpdateExpedienteDto dto, CancellationToken cancellationToken = default)
    {
        var expedienteExistente = await _obtenerExpedientePorIdRepository.ObtenerPorIdAsync(dto.IdentificacionEstudiante, cancellationToken);

        if (expedienteExistente == null)
        {
            return false;
        }

        await _actualizarExpedienteRepository.ActualizarAsync(dto, cancellationToken);
        return true;
    }
}
