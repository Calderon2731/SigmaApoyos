namespace SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IEliminarTipoDocumentoService;

public interface IEliminarTipoDocumentoService
{
    Task<bool> EliminarAsync(int idTipoDocumento, CancellationToken cancellationToken = default);
}
