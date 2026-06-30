using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Interfaces;

public interface IDocumentoRepository
{
    Task<IReadOnlyList<Documento>> ObtenerPorExpedienteAsync(string identificacionEstudiante, CancellationToken cancellationToken = default);

    Task AgregarAsync(Documento documento, CancellationToken cancellationToken = default);
}
