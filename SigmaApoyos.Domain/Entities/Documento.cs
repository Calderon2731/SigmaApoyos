namespace SigmaApoyos.Domain.Entities;

public class Documento
{
    public int IdDocumento { get; set; }

    public string ExpedienteId { get; set; } = string.Empty;

    public int TipoDocumentoId { get; set; }

    public string UsuarioId { get; set; } = string.Empty;

    public int Consecutivo { get; set; }

    public string RutaArchivo { get; set; } = string.Empty;

    public DateTime FechaSubida { get; set; }

    public int IdEstado { get; set; }

    public Expediente? Expediente { get; set; }

    public TipoDocumento? TipoDocumento { get; set; }

    public Estado? Estado { get; set; }
}
