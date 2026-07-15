using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;

public interface ICrearTipoDocumentoRepository
{
    Task CrearAsync(TipoDocumento tipoDocumento, CancellationToken cancellationToken = default);
}
