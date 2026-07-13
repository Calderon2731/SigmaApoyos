namespace SigmaApoyos.Application.DTOs.Documentos;

public class DocumentoDto
{
    public int IdDocumento { get; set; }

    public string IdentificacionEstudiante { get; set; } = string.Empty;

    public int TipoDocumentoId { get; set; }

    public string TipoDocumento { get; set; } = string.Empty;

    public string UsuarioId { get; set; } = string.Empty;

    public string Consecutivo { get; set; } = string.Empty;

    public string RutaArchivo { get; set; } = string.Empty;

    public DateTime FechaSubida { get; set; }

    public int IdEstado { get; set; }

    public string Estado { get; set; } = string.Empty;
}
