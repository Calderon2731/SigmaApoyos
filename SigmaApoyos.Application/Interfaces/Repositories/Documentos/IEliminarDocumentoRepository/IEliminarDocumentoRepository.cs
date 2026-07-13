namespace SigmaApoyos.Application.Interfaces.Repositories.Documentos;

public interface IEliminarDocumentoRepository
{
    Task EliminarAsync(int idDocumento, CancellationToken cancellationToken = default);
}
