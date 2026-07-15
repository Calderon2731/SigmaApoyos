namespace SigmaApoyos.Application.Interfaces.Repositories.Estados;

public interface IEliminarEstadoRepository
{
    Task EliminarAsync(int idEstado, CancellationToken cancellationToken = default);
}
