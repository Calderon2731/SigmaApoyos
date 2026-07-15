using SigmaApoyos.Application.DTOs.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IActualizarTipoDocumentoService;

namespace SigmaApoyos.Application.Services.TiposDocumento.ActualizarTipoDocumento;

public sealed class ActualizarTipoDocumento : IActualizarTipoDocumentoService
{
    private readonly IActualizarTipoDocumentoRepository _actualizarRepository;
    private readonly IObtenerTipoDocumentoPorIdRepository _obtenerRepository;
    public ActualizarTipoDocumento(IActualizarTipoDocumentoRepository actualizarRepository, IObtenerTipoDocumentoPorIdRepository obtenerRepository)
    {
        _actualizarRepository = actualizarRepository;
        _obtenerRepository = obtenerRepository;
    }
    public async Task<UpdateTipoDocumentoDto?> ObtenerParaEditarAsync(int idTipoDocumento, CancellationToken cancellationToken = default)
    {
        var tipo = await _obtenerRepository.ObtenerPorIdAsync(idTipoDocumento, cancellationToken);
        return tipo == null ? null : new UpdateTipoDocumentoDto { IdTipoDocumento = tipo.IdTipoDocumento, Tipo = tipo.Tipo };
    }
    public async Task<bool> ActualizarAsync(UpdateTipoDocumentoDto dto, CancellationToken cancellationToken = default)
    {
        if (await _obtenerRepository.ObtenerPorIdAsync(dto.IdTipoDocumento, cancellationToken) == null) return false;
        dto.Tipo = dto.Tipo.Trim();
        await _actualizarRepository.ActualizarAsync(dto, cancellationToken);
        return true;
    }
}
