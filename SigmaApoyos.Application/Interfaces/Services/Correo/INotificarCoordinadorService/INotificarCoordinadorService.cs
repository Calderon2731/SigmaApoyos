using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.DTOs.Expedientes;

namespace SigmaApoyos.Application.Interfaces.Services.Correo.INotificarCoordinadorService;

public interface INotificarCoordinadorService
{
    Task NotificarNuevoExpedienteAsync(
        CrearExpedienteDto expediente,
        CancellationToken cancellationToken = default);

    Task NotificarNuevoDocumentoAsync(
        CrearDocumentoDto documento,
        CancellationToken cancellationToken = default);

    Task NotificarNuevoUsuarioAsync(
        string nombreCompleto,
        string correo,
        string rol,
        CancellationToken cancellationToken = default);
}
