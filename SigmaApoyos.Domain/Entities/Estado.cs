using SigmaApoyos.Domain.Common;


namespace SigmaApoyos.Domain.Entities;
public class Estado : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Expediente> Expedientes { get; set; } = new List<Expediente>();

    public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
