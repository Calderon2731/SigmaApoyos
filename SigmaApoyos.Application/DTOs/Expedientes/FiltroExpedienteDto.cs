namespace SigmaApoyos.Application.DTOs.Expedientes;

public class FiltroExpedienteDto
{
    public string? Identificacion { get; set; }

    public int? IdEstado { get; set; }

    public int? IdTipoAdecuacion { get; set; }

    public int Pagina { get; set; } = 1;
}
