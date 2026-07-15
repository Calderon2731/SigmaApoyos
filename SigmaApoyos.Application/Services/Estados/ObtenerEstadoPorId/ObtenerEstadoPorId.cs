using SigmaApoyos.Application.DTOs.Estados;
using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Application.Interfaces.Services.Estado.IObtenerEstadoPorIdService;

namespace SigmaApoyos.Application.Services.Estados.ObtenerEstadoPorId;

public sealed class ObtenerEstadoPorId : IObtenerEstadoPorIdService
{
    private readonly IObtenerEstadoPorIdRepository _repository;

    public ObtenerEstadoPorId(IObtenerEstadoPorIdRepository repository) => _repository = repository;

    public async Task<EstadoDto?> ObtenerPorIdAsync(int idEstado, CancellationToken cancellationToken = default)
    {
        var estado = await _repository.ObtenerPorIdAsync(idEstado, cancellationToken);
        return estado == null ? null : new EstadoDto { IdEstado = estado.IdEstado, Nombre = estado.Nombre };
    }
}
