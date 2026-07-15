using SigmaApoyos.Application.DTOs.TiposDocumento;

namespace SigmaApoyos.Application.Interfaces.Services.TipoDocumento.ICrearTipoDocumentoService;

public interface ICrearTipoDocumentoService
{
    Task<bool> CrearAsync(CrearTipoDocumentoDto dto, CancellationToken cancellationToken = default);
}
