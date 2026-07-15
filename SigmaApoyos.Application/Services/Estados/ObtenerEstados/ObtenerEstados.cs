using SigmaApoyos.Application.DTOs.Estados;
using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Application.Interfaces.Services.Estado.IObtenerEstadoService;

namespace SigmaApoyos.Application.Services.Estados.ObtenerEstados;

public sealed class ObtenerEstados : IObtenerEstadoService
{
    private readonly IObtenerEstadosRepository _repository;

    public ObtenerEstados(IObtenerEstadosRepository repository) => _repository = repository;

    public async Task<IReadOnlyList<EstadoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        => await _repository.ObtenerTodosAsync(cancellationToken);
}
