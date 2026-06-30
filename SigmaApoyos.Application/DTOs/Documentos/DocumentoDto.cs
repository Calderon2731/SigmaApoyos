namespace SigmaApoyos.Application.DTOs.Documentos;

public class DocumentoDto
{
    public int IdDocumento { get; set; }

    public string ExpedienteId { get; set; } = string.Empty;

    public string TipoDocumento { get; set; } = string.Empty;

    public string RutaArchivo { get; set; } = string.Empty;

    public DateTime FechaSubida { get; set; }
}
