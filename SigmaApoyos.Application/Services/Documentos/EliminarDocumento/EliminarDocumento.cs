using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Application.Interfaces.Services.Documento.IEliminarDocumentoService;

namespace SigmaApoyos.Application.Services.Documentos.EliminarDocumento;

public sealed class EliminarDocumento : IEliminarDocumentoService
{
    private readonly IEliminarDocumentoRepository _eliminarDocumentoRepository;
    private readonly IObtenerDocumentoPorIdRepository _obtenerDocumentoPorIdRepository;

    public EliminarDocumento(
        IEliminarDocumentoRepository eliminarDocumentoRepository,
        IObtenerDocumentoPorIdRepository obtenerDocumentoPorIdRepository)
    {
        _eliminarDocumentoRepository = eliminarDocumentoRepository;
        _obtenerDocumentoPorIdRepository = obtenerDocumentoPorIdRepository;
    }

    public async Task<bool> EliminarAsync(int idDocumento, CancellationToken cancellationToken = default)
    {
        var documento = await _obtenerDocumentoPorIdRepository.ObtenerPorIdAsync(idDocumento, cancellationToken);

        if (documento == null)
        {
            return false;
        }

        await _eliminarDocumentoRepository.EliminarAsync(idDocumento, cancellationToken);
        return true;
    }
}
