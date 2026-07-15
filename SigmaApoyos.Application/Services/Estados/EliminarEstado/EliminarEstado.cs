using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Application.Interfaces.Services.Estado.IEliminarEstadoService;

namespace SigmaApoyos.Application.Services.Estados.EliminarEstado;

public sealed class EliminarEstado : IEliminarEstadoService
{
    private readonly IEliminarEstadoRepository _eliminarRepository;
    private readonly IObtenerEstadoPorIdRepository _obtenerRepository;

    public EliminarEstado(IEliminarEstadoRepository eliminarRepository, IObtenerEstadoPorIdRepository obtenerRepository)
    {
        _eliminarRepository = eliminarRepository;
        _obtenerRepository = obtenerRepository;
    }

    public async Task<bool> EliminarAsync(int idEstado, CancellationToken cancellationToken = default)
    {
        if (await _obtenerRepository.ObtenerPorIdAsync(idEstado, cancellationToken) == null) return false;
        await _eliminarRepository.EliminarAsync(idEstado, cancellationToken);
        return true;
    }
}
