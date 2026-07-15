using System.ComponentModel.DataAnnotations;

namespace SigmaApoyos.Application.DTOs.Estados;

public class CrearEstadoDto
{
    [Required(ErrorMessage = "El nombre del estado es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;
}
