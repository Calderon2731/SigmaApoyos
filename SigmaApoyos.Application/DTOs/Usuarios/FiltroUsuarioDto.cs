namespace SigmaApoyos.Application.DTOs.Usuarios;

public class FiltroUsuarioDto
{
    public string? Nombre { get; set; }

    public int? IdEstado { get; set; }

    public string? Rol { get; set; }

    public int Pagina { get; set; } = 1;
}
