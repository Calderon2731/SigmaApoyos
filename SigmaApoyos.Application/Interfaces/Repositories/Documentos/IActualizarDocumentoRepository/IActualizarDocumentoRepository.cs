using SigmaApoyos.Application.DTOs.Documentos;

namespace SigmaApoyos.Application.Interfaces.Repositories.Documentos;

public interface IActualizarDocumentoRepository
{
    Task ActualizarAsync(UpdateDocumentoDto dto, CancellationToken cancellationToken = default);
}
