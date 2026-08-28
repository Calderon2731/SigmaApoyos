using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Infrastructure.Correos
{
    public class CorreoSettings
    {
        public const string Seccion = "Correo";

        public string Servidor { get; set; } = string.Empty;
        public int Puerto { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string Remitente { get; set; } = string.Empty;
        public string NombreRemitente { get; set; } = "Sigma Apoyos";
        public bool UsarSsl { get; set; } = true;
    }
}
