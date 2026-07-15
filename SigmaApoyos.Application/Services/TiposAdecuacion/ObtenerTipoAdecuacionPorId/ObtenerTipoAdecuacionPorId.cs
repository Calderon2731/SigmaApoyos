using SigmaApoyos.Application.DTOs.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IObtenerTipoAdecuacionPorIdService;

namespace SigmaApoyos.Application.Services.TiposAdecuacion.ObtenerTipoAdecuacionPorId;

public sealed class ObtenerTipoAdecuacionPorId : IObtenerTipoAdecuacionPorIdService
{
    private readonly IObtenerTipoAdecuacionPorIdRepository _repository;
    public ObtenerTipoAdecuacionPorId(IObtenerTipoAdecuacionPorIdRepository repository) => _repository = repository;
    public async Task<TipoAdecuacionDto?> ObtenerPorIdAsync(int idTipoAdecuacion, CancellationToken cancellationToken = default)
    {
        var tipo = await _repository.ObtenerPorIdAsync(idTipoAdecuacion, cancellationToken);
        return tipo == null ? null : new TipoAdecuacionDto { IdTipoAdecuacion = tipo.IdTipoAdecuacion, Nombre = tipo.Nombre };
    }
}
