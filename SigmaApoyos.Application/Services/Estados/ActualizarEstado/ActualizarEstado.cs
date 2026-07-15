using SigmaApoyos.Application.DTOs.Estados;
using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Application.Interfaces.Services.Estado.IActualizarEstadoService;

namespace SigmaApoyos.Application.Services.Estados.ActualizarEstado;

public sealed class ActualizarEstado : IActualizarEstadoService
{
    private readonly IActualizarEstadoRepository _actualizarRepository;
    private readonly IObtenerEstadoPorIdRepository _obtenerRepository;

    public ActualizarEstado(IActualizarEstadoRepository actualizarRepository, IObtenerEstadoPorIdRepository obtenerRepository)
    {
        _actualizarRepository = actualizarRepository;
        _obtenerRepository = obtenerRepository;
    }

    public async Task<UpdateEstadoDto?> ObtenerParaEditarAsync(int idEstado, CancellationToken cancellationToken = default)
    {
        var estado = await _obtenerRepository.ObtenerPorIdAsync(idEstado, cancellationToken);
        return estado == null ? null : new UpdateEstadoDto { IdEstado = estado.IdEstado, Nombre = estado.Nombre };
    }

    public async Task<bool> ActualizarAsync(UpdateEstadoDto dto, CancellationToken cancellationToken = default)
    {
        if (await _obtenerRepository.ObtenerPorIdAsync(dto.IdEstado, cancellationToken) == null) return false;
        dto.Nombre = dto.Nombre.Trim();
        await _actualizarRepository.ActualizarAsync(dto, cancellationToken);
        return true;
    }
}
