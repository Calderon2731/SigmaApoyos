using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IEliminarExpedienteRepository
{
    public interface IEliminarExpedienteRepository
    {
        Task EliminarAsync(Expediente expediente, CancellationToken cancellationToken = default);
    }
}
