using Microsoft.AspNetCore.Identity;
using SigmaApoyos.Application.DTOs.Correos;
using SigmaApoyos.Application.Interfaces.Services.Correo.ICorreoService;
using SigmaApoyos.Infrastructure.Identity;
using System.Net;

namespace SigmaApoyos.Infrastructure.Correos;

public sealed class IdentityEmailSender : IEmailSender<ApplicationUser>
{
    private readonly ICorreoService _correoService;

    public IdentityEmailSender(ICorreoService correoService)
    {
        _correoService = correoService;
    }

    public Task SendConfirmationLinkAsync(
        ApplicationUser user,
        string email,
        string confirmationLink)
    {
        var enlace = WebUtility.HtmlEncode(confirmationLink);

        return EnviarAsync(
            email,
            "Confirma tu cuenta de Sigma Apoyos",
            $"<h2>Confirma tu cuenta</h2><p>Hola {WebUtility.HtmlEncode(user.Nombre)}, utiliza el siguiente enlace para confirmar tu cuenta:</p><p><a href=\"{enlace}\">Confirmar cuenta</a></p>");
    }

    public Task SendPasswordResetLinkAsync(
        ApplicationUser user,
        string email,
        string resetLink)
    {
        var enlace = WebUtility.HtmlEncode(resetLink);

        return EnviarAsync(
            email,
            "Restablece tu contraseña de Sigma Apoyos",
            $"<h2>Restablecimiento de contraseña</h2><p>Hola {WebUtility.HtmlEncode(user.Nombre)}, recibimos una solicitud para cambiar tu contraseña.</p><p><a href=\"{enlace}\">Crear una nueva contraseña</a></p><p>Si no realizaste esta solicitud, puedes ignorar este correo.</p>");
    }

    public Task SendPasswordResetCodeAsync(
        ApplicationUser user,
        string email,
        string resetCode)
    {
        return EnviarAsync(
            email,
            "Código para restablecer tu contraseña",
            $"<h2>Código de recuperación</h2><p>Hola {WebUtility.HtmlEncode(user.Nombre)}, tu código es:</p><p><strong>{WebUtility.HtmlEncode(resetCode)}</strong></p>");
    }

    private Task EnviarAsync(string destinatario, string asunto, string cuerpoHtml)
    {
        return _correoService.EnviarAsync(new CorreoDto
        {
            Destinatarios = [destinatario],
            Asunto = asunto,
            CuerpoHtml = cuerpoHtml
        });
    }
}
