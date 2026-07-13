using System.ComponentModel.DataAnnotations;

namespace SigmaApoyos.Application.DTOs.Usuarios;

public class UpdateUsuarioDto
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El primer apellido es obligatorio.")]
    [StringLength(100)]
    public string PrimerApellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El segundo apellido es obligatorio.")]
    [StringLength(100)]
    public string SegundoApellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El rol es obligatorio.")]
    public string RoleName { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado válido.")]
    public int IdEstado { get; set; }
}
