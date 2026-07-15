using SigmaApoyos.Application.DTOs.TiposDocumento;

namespace SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IActualizarTipoDocumentoService;

public interface IActualizarTipoDocumentoService
{
    Task<UpdateTipoDocumentoDto?> ObtenerParaEditarAsync(int idTipoDocumento, CancellationToken cancellationToken = default);
    Task<bool> ActualizarAsync(UpdateTipoDocumentoDto dto, CancellationToken cancellationToken = default);
}
