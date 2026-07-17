namespace SigmaApoyos.Application.DTOs.Documentos;

public class FiltroDocumentoDto
{
    public int? TipoDocumentoId { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? Fecha { get; set; }

    public int Pagina { get; set; } = 1;
}
