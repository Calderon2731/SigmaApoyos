namespace SigmaApoyos.Domain.Entities;

public class Expediente
{
    public string IdentificacionEstudiante { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string PrimerApellido { get; set; } = string.Empty;

    public string SegundoApellido { get; set; } = string.Empty;

    public DateOnly FechaNacimiento { get; set; }

    public string NombreEncargado { get; set; } = string.Empty;

    public string TelefonoEncargado { get; set; } = string.Empty;

    public string Observaciones { get; set; } = string.Empty;

    public int IdTipoAdecuacion { get; set; }

    public int IdEstado { get; set; }

    public TipoAdecuacion? TipoAdecuacion { get; set; }

    public Estado? Estado { get; set; }

    public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
