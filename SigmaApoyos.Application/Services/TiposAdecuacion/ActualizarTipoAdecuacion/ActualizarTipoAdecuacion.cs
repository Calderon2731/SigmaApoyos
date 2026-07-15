using SigmaApoyos.Application.DTOs.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IActualizarTipoAdecuacionService;

namespace SigmaApoyos.Application.Services.TiposAdecuacion.ActualizarTipoAdecuacion;

public sealed class ActualizarTipoAdecuacion : IActualizarTipoAdecuacionService
{
    private readonly IActualizarTipoAdecuacionRepository _actualizarRepository;
    private readonly IObtenerTipoAdecuacionPorIdRepository _obtenerRepository;
    public ActualizarTipoAdecuacion(IActualizarTipoAdecuacionRepository actualizarRepository, IObtenerTipoAdecuacionPorIdRepository obtenerRepository)
    {
        _actualizarRepository = actualizarRepository;
        _obtenerRepository = obtenerRepository;
    }
    public async Task<UpdateTipoAdecuacionDto?> ObtenerParaEditarAsync(int idTipoAdecuacion, CancellationToken cancellationToken = default)
    {
        var tipo = await _obtenerRepository.ObtenerPorIdAsync(idTipoAdecuacion, cancellationToken);
        return tipo == null ? null : new UpdateTipoAdecuacionDto { IdTipoAdecuacion = tipo.IdTipoAdecuacion, Nombre = tipo.Nombre };
    }
    public async Task<bool> ActualizarAsync(UpdateTipoAdecuacionDto dto, CancellationToken cancellationToken = default)
    {
        if (await _obtenerRepository.ObtenerPorIdAsync(dto.IdTipoAdecuacion, cancellationToken) == null) return false;
        dto.Nombre = dto.Nombre.Trim();
        await _actualizarRepository.ActualizarAsync(dto, cancellationToken);
        return true;
    }
}
