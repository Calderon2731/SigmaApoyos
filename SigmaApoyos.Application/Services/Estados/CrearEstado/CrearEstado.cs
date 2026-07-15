using SigmaApoyos.Application.DTOs.Estados;
using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Application.Interfaces.Services.Estado.ICrearEstadoService;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Services.Estados.CrearEstado;

public sealed class CrearEstado : ICrearEstadoService
{
    private readonly ICrearEstadoRepository _repository;

    public CrearEstado(ICrearEstadoRepository repository) => _repository = repository;

    public async Task<bool> CrearAsync(CrearEstadoDto dto, CancellationToken cancellationToken = default)
    {
        await _repository.CrearAsync(new Estado { Nombre = dto.Nombre.Trim() }, cancellationToken);
        return true;
    }
}
