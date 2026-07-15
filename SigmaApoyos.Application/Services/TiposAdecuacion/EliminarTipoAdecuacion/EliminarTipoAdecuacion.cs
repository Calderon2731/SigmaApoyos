using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IEliminarTipoAdecuacionService;

namespace SigmaApoyos.Application.Services.TiposAdecuacion.EliminarTipoAdecuacion;

public sealed class EliminarTipoAdecuacion : IEliminarTipoAdecuacionService
{
    private readonly IEliminarTipoAdecuacionRepository _eliminarRepository;
    private readonly IObtenerTipoAdecuacionPorIdRepository _obtenerRepository;
    public EliminarTipoAdecuacion(IEliminarTipoAdecuacionRepository eliminarRepository, IObtenerTipoAdecuacionPorIdRepository obtenerRepository)
    {
        _eliminarRepository = eliminarRepository;
        _obtenerRepository = obtenerRepository;
    }
    public async Task<bool> EliminarAsync(int idTipoAdecuacion, CancellationToken cancellationToken = default)
    {
        if (await _obtenerRepository.ObtenerPorIdAsync(idTipoAdecuacion, cancellationToken) == null) return false;
        await _eliminarRepository.EliminarAsync(idTipoAdecuacion, cancellationToken);
        return true;
    }
}
