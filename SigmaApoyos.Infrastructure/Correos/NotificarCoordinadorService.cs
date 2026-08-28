using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SigmaApoyos.Application.DTOs.Correos;
using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Services.Correo.ICorreoService;
using SigmaApoyos.Application.Interfaces.Services.Correo.INotificarCoordinadorService;
using SigmaApoyos.Infrastructure.Identity;
using System.Net;

namespace SigmaApoyos.Infrastructure.Correos;

public sealed class NotificarCoordinadorService : INotificarCoordinadorService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICorreoService _correoService;
    private readonly ILogger<NotificarCoordinadorService> _logger;

    public NotificarCoordinadorService(
        UserManager<ApplicationUser> userManager,
        ICorreoService correoService,
        ILogger<NotificarCoordinadorService> logger)
    {
        _userManager = userManager;
        _correoService = correoService;
        _logger = logger;
    }

    public Task NotificarNuevoExpedienteAsync(
        CrearExpedienteDto expediente,
        CancellationToken cancellationToken = default)
    {
        var identificacion = WebUtility.HtmlEncode(expediente.IdentificacionEstudiante);
        var nombreCompleto = WebUtility.HtmlEncode(
            $"{expediente.Nombre} {expediente.PrimerApellido} {expediente.SegundoApellido}".Trim());

        return EnviarAsync(
            $"Nuevo expediente registrado - {expediente.IdentificacionEstudiante}",
            $"<h2>Nuevo expediente registrado</h2><p><strong>Estudiante:</strong> {nombreCompleto}</p><p><strong>Identificación:</strong> {identificacion}</p>",
            cancellationToken);
    }

    public Task NotificarNuevoDocumentoAsync(
        CrearDocumentoDto documento,
        CancellationToken cancellationToken = default)
    {
        var identificacion = WebUtility.HtmlEncode(documento.IdentificacionEstudiante);
        var consecutivo = WebUtility.HtmlEncode(documento.Consecutivo);

        return EnviarAsync(
            $"Nuevo documento agregado - {documento.IdentificacionEstudiante}",
            $"<h2>Nuevo documento agregado</h2><p><strong>Expediente:</strong> {identificacion}</p><p><strong>Consecutivo:</strong> {consecutivo}</p>",
            cancellationToken);
    }

    public Task NotificarNuevoUsuarioAsync(
        string nombreCompleto,
        string correo,
        string rol,
        CancellationToken cancellationToken = default)
    {
        return EnviarAsync(
            "Nuevo usuario registrado en Sigma Apoyos",
            $"<h2>Nuevo usuario registrado</h2><p><strong>Nombre:</strong> {WebUtility.HtmlEncode(nombreCompleto)}</p><p><strong>Correo:</strong> {WebUtility.HtmlEncode(correo)}</p><p><strong>Rol:</strong> {WebUtility.HtmlEncode(rol)}</p>",
            cancellationToken);
    }

    private async Task EnviarAsync(
        string asunto,
        string cuerpoHtml,
        CancellationToken cancellationToken)
    {
        try
        {
            var coordinadores = await _userManager.GetUsersInRoleAsync(IdentityRoles.CoordinadorAcademico);
            var destinatarios = coordinadores
                .Where(usuario => usuario.IdEstado == 2 && !string.IsNullOrWhiteSpace(usuario.Email))
                .Select(usuario => usuario.Email!)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (destinatarios.Count == 0)
            {
                _logger.LogWarning("No hay coordinadores académicos activos con correo para recibir la notificación.");
                return;
            }

            await _correoService.EnviarAsync(new CorreoDto
            {
                Destinatarios = destinatarios,
                Asunto = asunto,
                CuerpoHtml = cuerpoHtml
            }, cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "No se pudo enviar la notificación al coordinador académico.");
        }
    }
}
