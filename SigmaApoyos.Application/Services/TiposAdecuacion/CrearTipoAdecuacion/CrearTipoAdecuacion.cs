using SigmaApoyos.Application.DTOs.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.ICrearTipoAdecuacionService;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Services.TiposAdecuacion.CrearTipoAdecuacion;

public sealed class CrearTipoAdecuacion : ICrearTipoAdecuacionService
{
    private readonly ICrearTipoAdecuacionRepository _repository;
    public CrearTipoAdecuacion(ICrearTipoAdecuacionRepository repository) => _repository = repository;
    public async Task<bool> CrearAsync(CrearTipoAdecuacionDto dto, CancellationToken cancellationToken = default)
    {
        await _repository.CrearAsync(new TipoAdecuacion { Nombre = dto.Nombre.Trim() }, cancellationToken);
        return true;
    }
}
