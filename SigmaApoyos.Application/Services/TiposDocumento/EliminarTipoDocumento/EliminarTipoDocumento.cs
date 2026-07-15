using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IEliminarTipoDocumentoService;

namespace SigmaApoyos.Application.Services.TiposDocumento.EliminarTipoDocumento;

public sealed class EliminarTipoDocumento : IEliminarTipoDocumentoService
{
    private readonly IEliminarTipoDocumentoRepository _eliminarRepository;
    private readonly IObtenerTipoDocumentoPorIdRepository _obtenerRepository;
    public EliminarTipoDocumento(IEliminarTipoDocumentoRepository eliminarRepository, IObtenerTipoDocumentoPorIdRepository obtenerRepository)
    {
        _eliminarRepository = eliminarRepository;
        _obtenerRepository = obtenerRepository;
    }
    public async Task<bool> EliminarAsync(int idTipoDocumento, CancellationToken cancellationToken = default)
    {
        if (await _obtenerRepository.ObtenerPorIdAsync(idTipoDocumento, cancellationToken) == null) return false;
        await _eliminarRepository.EliminarAsync(idTipoDocumento, cancellationToken);
        return true;
    }
}
