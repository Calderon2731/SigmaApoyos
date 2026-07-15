namespace SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;

public interface IEliminarTipoAdecuacionRepository
{
    Task EliminarAsync(int idTipoAdecuacion, CancellationToken cancellationToken = default);
}
