using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;

public interface IObtenerTipoDocumentoPorIdRepository
{
    Task<TipoDocumento?> ObtenerPorIdAsync(int idTipoDocumento, CancellationToken cancellationToken = default);
}
