using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.DTOs.Expedientes;
public class CrearExpedienteDto
{
    public string IdentificacionEstudiante { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string PrimerApellido { get; set; } = string.Empty;

    public string SegundoApellido { get; set; } = string.Empty;

    public DateOnly FechaNacimiento { get; set; }

    public string NombreEncargado { get; set; } = string.Empty;

    public string TelefonoEncargado { get; set; } = string.Empty;

    public string Observaciones { get; set; } = string.Empty;

    public int IdTipoAdecuacion { get; set; }

    public int IdEstado { get; set; }

}
