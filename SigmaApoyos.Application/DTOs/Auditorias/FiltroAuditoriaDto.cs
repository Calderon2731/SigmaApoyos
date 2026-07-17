namespace SigmaApoyos.Application.DTOs.Auditorias;

public class FiltroAuditoriaDto
{
    public string? Usuario { get; set; }

    public string? Accion { get; set; }

    public string? Entidad { get; set; }

    public DateTime? FechaDesde { get; set; }

    public DateTime? FechaHasta { get; set; }

    public int Pagina { get; set; } = 1;
}
