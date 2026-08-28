using Microsoft.Extensions.Options;
using SigmaApoyos.Application.DTOs.Correos;
using SigmaApoyos.Application.Interfaces.Services.Correo.ICorreoService;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Utils;

namespace SigmaApoyos.Infrastructure.Correos
{
    public class CorreoService : ICorreoService
    {
        private readonly CorreoSettings _settings;
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment _hostEnvironment;

        public CorreoService(IOptions<CorreoSettings> options, Microsoft.Extensions.Hosting.IHostEnvironment hostEnvironment){
        
         _settings = options.Value;
         _hostEnvironment = hostEnvironment;
        }

        public async Task EnviarAsync(CorreoDto correo, CancellationToken cancellationToken = default)
        {
            var mensaje = new MimeMessage();

            mensaje.From.Add(
                new MailboxAddress(_settings.NombreRemitente, _settings.Remitente)
                );

            foreach (var destinatario in correo.Destinatarios)
            {
                mensaje.To.Add(
                    new MailboxAddress(string.Empty, destinatario));
            }

            mensaje.Subject = correo.Asunto;
            var cuerpo = new BodyBuilder
            {
                HtmlBody = CrearPlantillaCorreo(correo.CuerpoHtml)
            };

            var rutaLogo = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", "images", "sigma-apoyos-logo.png");
            if (File.Exists(rutaLogo))
            {
                var logo = cuerpo.LinkedResources.Add(rutaLogo);
                logo.ContentId = "sigma-apoyos-logo";
                logo.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);
            }

            mensaje.Body = cuerpo.ToMessageBody();

            using var cliente = new SmtpClient();

            await cliente.ConnectAsync
                (
                _settings.Servidor,
                 _settings.Puerto,
                 _settings.UsarSsl
                 ? SecureSocketOptions.Auto
                 : SecureSocketOptions.None,
                  cancellationToken
                );

            await cliente.AuthenticateAsync
            (
               _settings.Usuario,
               _settings.Clave,
               cancellationToken
             );

            await cliente.SendAsync(mensaje, cancellationToken);
            await cliente.DisconnectAsync(true, cancellationToken);


        }

        private static string CrearPlantillaCorreo(string contenido)
        {
            return $"""
                <!DOCTYPE html>
                <html lang="es">
                <body style="margin:0;padding:0;background:#f4f7fa;font-family:Arial,sans-serif;color:#243b53;">
                    <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="padding:32px 16px;">
                        <tr><td align="center">
                            <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="max-width:600px;background:#ffffff;border-radius:16px;overflow:hidden;">
                                <tr><td style="padding:24px 32px;background:#102a43;">
                                    <img src="cid:sigma-apoyos-logo" alt="Sigma Apoyos" width="210" style="display:block;width:210px;height:auto;border-radius:8px;background:#ffffff;padding:5px 9px;" />
                                </td></tr>
                                <tr><td style="padding:32px;line-height:1.55;">{contenido}</td></tr>
                                <tr><td style="padding:18px 32px;background:#e9f6f7;color:#486581;font-size:12px;">Sigma Apoyos · Liceo de Calle Fallas</td></tr>
                            </table>
                        </td></tr>
                    </table>
                </body>
                </html>
                """;
        }
    }
}
