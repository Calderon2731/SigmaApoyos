using SigmaApoyos.Application.DTOs.TiposAdecuacion;

namespace SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.ICrearTipoAdecuacionService;

public interface ICrearTipoAdecuacionService
{
    Task<bool> CrearAsync(CrearTipoAdecuacionDto dto, CancellationToken cancellationToken = default);
}
