using SigmaApoyos.Application.DTOs.Expedientes;

namespace SigmaApoyos.Application.Interfaces.Services.Expediente.ICrearExpedienteService
{
    public interface ICrearExpedienteService
    {
        Task<bool> CrearAsync(CrearExpedienteDto dto, CancellationToken cancellationToken = default);
    }
}
