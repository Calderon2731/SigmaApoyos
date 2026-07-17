using SigmaApoyos.Application.DTOs.Expedientes;

namespace SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedienteIntegralService;

public interface IObtenerExpedienteIntegralService
{
    Task<ExpedienteIntegralDto?> ObtenerAsync(string identificacionEstudiante, CancellationToken cancellationToken = default);
}
