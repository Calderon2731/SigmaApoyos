using SigmaApoyos.Application.DTOs.Estados;

namespace SigmaApoyos.Application.Interfaces.Services.Estado.ICrearEstadoService;

public interface ICrearEstadoService
{
    Task<bool> CrearAsync(CrearEstadoDto dto, CancellationToken cancellationToken = default);
}
