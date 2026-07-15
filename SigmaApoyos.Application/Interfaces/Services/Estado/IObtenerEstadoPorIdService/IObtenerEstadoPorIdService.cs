using SigmaApoyos.Application.DTOs.Estados;

namespace SigmaApoyos.Application.Interfaces.Services.Estado.IObtenerEstadoPorIdService;

public interface IObtenerEstadoPorIdService
{
    Task<EstadoDto?> ObtenerPorIdAsync(int idEstado, CancellationToken cancellationToken = default);
}
