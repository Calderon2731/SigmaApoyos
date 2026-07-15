namespace SigmaApoyos.Domain.Entities;

public class Auditoria
{
    public long IdAuditoria { get; set; }
    public string? UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
    public string Accion { get; set; } = string.Empty;
    public string Entidad { get; set; } = string.Empty;
    public string RegistroId { get; set; } = string.Empty;
    public string? ValoresAnteriores { get; set; }
    public string? ValoresNuevos { get; set; }
    public DateTime FechaUtc { get; set; }
    public string? DireccionIp { get; set; }
    public string? Ruta { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}
