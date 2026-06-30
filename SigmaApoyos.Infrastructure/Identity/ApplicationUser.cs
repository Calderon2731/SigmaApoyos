using Microsoft.AspNetCore.Identity;

namespace SigmaApoyos.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string Nombre { get; set; } = string.Empty;

    public string PrimerApellido { get; set; } = string.Empty;

    public string SegundoApellido { get; set; } = string.Empty;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public int IdEstado { get; set; }
}
