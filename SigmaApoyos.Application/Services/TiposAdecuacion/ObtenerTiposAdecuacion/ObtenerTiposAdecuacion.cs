using SigmaApoyos.Application.DTOs.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IObtenerTipoAdecuacionService;

namespace SigmaApoyos.Application.Services.TiposAdecuacion.ObtenerTiposAdecuacion;

public sealed class ObtenerTiposAdecuacion : IObtenerTipoAdecuacionService
{
    private readonly IObtenerTiposAdecuacionRepository _repository;
    public ObtenerTiposAdecuacion(IObtenerTiposAdecuacionRepository repository) => _repository = repository;
    public async Task<IReadOnlyList<TipoAdecuacionDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        => await _repository.ObtenerTodosAsync(cancellationToken);
}
