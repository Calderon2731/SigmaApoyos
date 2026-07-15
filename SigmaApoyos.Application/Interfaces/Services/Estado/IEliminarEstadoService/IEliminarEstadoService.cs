namespace SigmaApoyos.Application.Interfaces.Services.Estado.IEliminarEstadoService;

public interface IEliminarEstadoService
{
    Task<bool> EliminarAsync(int idEstado, CancellationToken cancellationToken = default);
}
