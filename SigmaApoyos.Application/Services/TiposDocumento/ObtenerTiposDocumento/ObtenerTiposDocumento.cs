using SigmaApoyos.Application.DTOs.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IObtenerTipoDocumentoService;

namespace SigmaApoyos.Application.Services.TiposDocumento.ObtenerTiposDocumento;

public sealed class ObtenerTiposDocumento : IObtenerTipoDocumentoService
{
    private readonly IObtenerTiposDocumentoRepository _repository;
    public ObtenerTiposDocumento(IObtenerTiposDocumentoRepository repository) => _repository = repository;
    public async Task<IReadOnlyList<TipoDocumentoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        => await _repository.ObtenerTodosAsync(cancellationToken);
}
