using System.ComponentModel.DataAnnotations;

namespace SigmaApoyos.Application.DTOs.TiposDocumento;

public class CrearTipoDocumentoDto
{
    [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
    [StringLength(150, ErrorMessage = "El tipo no puede superar los 150 caracteres.")]
    [Display(Name = "Tipo de documento")]
    public string Tipo { get; set; } = string.Empty;
}
