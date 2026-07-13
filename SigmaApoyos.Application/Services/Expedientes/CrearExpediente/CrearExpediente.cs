using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Services.Expediente.ICrearExpedienteService;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Services.Expedientes.CrearExpediente;

public sealed class CrearExpediente : ICrearExpedienteService
{
    private readonly ICrearExpedienteRepository _crearExpedienteRepository;
    private readonly IObtenerExpedientePorIdRepository _obtenerExpedientePorIdRepository;

    public CrearExpediente(
        ICrearExpedienteRepository crearExpedienteRepository,
        IObtenerExpedientePorIdRepository obtenerExpedientePorIdRepository)
    {
        _crearExpedienteRepository = crearExpedienteRepository;
        _obtenerExpedientePorIdRepository = obtenerExpedientePorIdRepository;
    }

    public async Task<bool> CrearAsync(CrearExpedienteDto dto, CancellationToken cancellationToken = default)
    {
        var expedienteExistente = await _obtenerExpedientePorIdRepository.ObtenerPorIdAsync(dto.IdentificacionEstudiante, cancellationToken);

        if (expedienteExistente != null)
        {
            return false;
        }

        var expediente = new Expediente
        {
            IdentificacionEstudiante = dto.IdentificacionEstudiante,
            Nombre = dto.Nombre,
            PrimerApellido = dto.PrimerApellido,
            SegundoApellido = dto.SegundoApellido,
            FechaNacimiento = dto.FechaNacimiento.ToDateTime(TimeOnly.MinValue),
            NombreEncargado = dto.NombreEncargado,
            TelefonoEncargado = dto.TelefonoEncargado,
            Observaciones = dto.Observaciones,
            IdTipoAdecuacion = dto.IdTipoAdecuacion,
            IdEstado = dto.IdEstado
        };

        await _crearExpedienteRepository.CrearAsync(expediente, cancellationToken);
        return true;
    }
}
