namespace SigmaApoyos.Domain.Entities;

public class TipoAdecuacion
{
    public int IdTipoAdecuacion { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public ICollection<Expediente> Expedientes { get; set; } = new List<Expediente>();
}
