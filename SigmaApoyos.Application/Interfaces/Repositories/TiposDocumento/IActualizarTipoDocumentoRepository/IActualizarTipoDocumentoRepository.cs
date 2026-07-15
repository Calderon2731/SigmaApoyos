using SigmaApoyos.Application.DTOs.TiposDocumento;

namespace SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;

public interface IActualizarTipoDocumentoRepository
{
    Task ActualizarAsync(UpdateTipoDocumentoDto dto, CancellationToken cancellationToken = default);
}
