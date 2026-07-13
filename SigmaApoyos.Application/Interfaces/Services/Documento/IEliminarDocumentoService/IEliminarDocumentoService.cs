namespace SigmaApoyos.Application.Interfaces.Services.Documento.IEliminarDocumentoService;

public interface IEliminarDocumentoService
{
    Task<bool> EliminarAsync(int idDocumento, CancellationToken cancellationToken = default);
}
