namespace SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IEliminarTipoAdecuacionService;

public interface IEliminarTipoAdecuacionService
{
    Task<bool> EliminarAsync(int idTipoAdecuacion, CancellationToken cancellationToken = default);
}
