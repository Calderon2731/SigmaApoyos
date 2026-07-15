namespace SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;

public interface IEliminarTipoDocumentoRepository
{
    Task EliminarAsync(int idTipoDocumento, CancellationToken cancellationToken = default);
}
