namespace SigmaApoyos.Application.Interfaces.Services.Expediente.IEliminarExpedienteService
{
    public interface IEliminarExpedienteService
    {
        Task<bool> EliminarAsync(string identificacionEstudiante, CancellationToken cancellationToken = default);
    }
}
