using SigmaApoyos.Application.DTOs.TiposAdecuacion;

namespace SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IActualizarTipoAdecuacionService;

public interface IActualizarTipoAdecuacionService
{
    Task<UpdateTipoAdecuacionDto?> ObtenerParaEditarAsync(int idTipoAdecuacion, CancellationToken cancellationToken = default);
    Task<bool> ActualizarAsync(UpdateTipoAdecuacionDto dto, CancellationToken cancellationToken = default);
}
