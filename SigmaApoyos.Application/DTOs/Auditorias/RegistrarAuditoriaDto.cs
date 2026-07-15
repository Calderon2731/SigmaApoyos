namespace SigmaApoyos.Application.DTOs.Auditorias;

public class RegistrarAuditoriaDto
{
    public string? UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
    public string Accion { get; set; } = string.Empty;
    public string Entidad { get; set; } = string.Empty;
    public string RegistroId { get; set; } = string.Empty;
    public string? DireccionIp { get; set; }
    public string? Ruta { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}
