using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Interfaces.Repositories.Documentos;

public interface ICrearDocumentoRepository
{
    Task CrearAsync(Documento documento, CancellationToken cancellationToken = default);
}
