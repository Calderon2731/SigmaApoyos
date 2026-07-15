using System.ComponentModel.DataAnnotations;

namespace SigmaApoyos.Application.DTOs.TiposAdecuacion;

public class UpdateTipoAdecuacionDto
{
    public int IdTipoAdecuacion { get; set; }

    [Required(ErrorMessage = "El nombre del tipo de adecuación es obligatorio.")]
    [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;
}
