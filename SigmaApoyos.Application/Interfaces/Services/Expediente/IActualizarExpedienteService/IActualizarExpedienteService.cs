using SigmaApoyos.Application.DTOs.Expedientes;

namespace SigmaApoyos.Application.Interfaces.Services.Expediente.IActualizarExpedienteService
{
    public interface IActualizarExpedienteService
    {
        Task<UpdateExpedienteDto?> ObtenerParaEditarAsync(string identificacion, CancellationToken cancellationToken = default);
        Task<bool> ActualizarAsync(UpdateExpedienteDto dto, CancellationToken cancellationToken = default);
    }
}
