using System.ComponentModel.DataAnnotations;

namespace SigmaApoyos.Application.DTOs.Documentos;

public class UpdateDocumentoDto
{
    public int IdDocumento { get; set; }

    [Required(ErrorMessage = "La identificación del estudiante es obligatoria.")]
    [StringLength(20, ErrorMessage = "La identificación no puede superar los 20 caracteres.")]
    public string IdentificacionEstudiante { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de documento válido.")]
    public int TipoDocumentoId { get; set; }

    [Required(ErrorMessage = "El consecutivo es obligatorio.")]
    [StringLength(50, ErrorMessage = "El consecutivo no puede superar los 50 caracteres.")]
    public string Consecutivo { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado válido.")]
    public int IdEstado { get; set; }

    public string RutaArchivo { get; set; } = string.Empty;
}
