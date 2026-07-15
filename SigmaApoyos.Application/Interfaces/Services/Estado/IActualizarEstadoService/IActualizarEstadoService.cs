using SigmaApoyos.Application.DTOs.Estados;

namespace SigmaApoyos.Application.Interfaces.Services.Estado.IActualizarEstadoService;

public interface IActualizarEstadoService
{
    Task<UpdateEstadoDto?> ObtenerParaEditarAsync(int idEstado, CancellationToken cancellationToken = default);
    Task<bool> ActualizarAsync(UpdateEstadoDto dto, CancellationToken cancellationToken = default);
}
