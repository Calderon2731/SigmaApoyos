namespace SigmaApoyos.Domain.Entities;

public class TipoDocumento
{
    public int IdTipoDocumento { get; set; }

    public string Tipo { get; set; } = string.Empty;

    public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
