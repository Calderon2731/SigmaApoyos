using SigmaApoyos.Application.DTOs.TiposAdecuacion;

namespace SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IObtenerTipoAdecuacionPorIdService;

public interface IObtenerTipoAdecuacionPorIdService
{
    Task<TipoAdecuacionDto?> ObtenerPorIdAsync(int idTipoAdecuacion, CancellationToken cancellationToken = default);
}
