using SigmaApoyos.Application.DTOs.Expedientes;

namespace SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IActualizarExpedienteRepository
{
    public interface IActualizarExpedienteRepository
    {
        Task ActualizarAsync(UpdateExpedienteDto dto, CancellationToken cancellationToken = default);
    }
}
