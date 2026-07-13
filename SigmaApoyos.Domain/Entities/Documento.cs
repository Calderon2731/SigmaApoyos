namespace SigmaApoyos.Domain.Entities;

public class Documento
{
    public int IdDocumento { get; set; }

    public string IdentificacionEstudiante { get; set; } = string.Empty;

    public int TipoDocumentoId { get; set; }

    public string UsuarioId { get; set; } = string.Empty;

    public string Consecutivo { get; set; } = string.Empty;

    public string RutaArchivo { get; set; } = string.Empty;

    public DateTime FechaSubida { get; set; }

    public int IdEstado { get; set; }

    public Expediente? Expediente { get; set; }

    public TipoDocumento? TipoDocumento { get; set; }

    public Estado? Estado { get; set; }
}
