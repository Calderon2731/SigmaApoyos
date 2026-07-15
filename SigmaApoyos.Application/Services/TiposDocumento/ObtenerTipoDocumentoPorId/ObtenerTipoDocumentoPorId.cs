using SigmaApoyos.Application.DTOs.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IObtenerTipoDocumentoPorIdService;

namespace SigmaApoyos.Application.Services.TiposDocumento.ObtenerTipoDocumentoPorId;

public sealed class ObtenerTipoDocumentoPorId : IObtenerTipoDocumentoPorIdService
{
    private readonly IObtenerTipoDocumentoPorIdRepository _repository;
    public ObtenerTipoDocumentoPorId(IObtenerTipoDocumentoPorIdRepository repository) => _repository = repository;
    public async Task<TipoDocumentoDto?> ObtenerPorIdAsync(int idTipoDocumento, CancellationToken cancellationToken = default)
    {
        var tipo = await _repository.ObtenerPorIdAsync(idTipoDocumento, cancellationToken);
        return tipo == null ? null : new TipoDocumentoDto { IdTipoDocumento = tipo.IdTipoDocumento, Tipo = tipo.Tipo };
    }
}
