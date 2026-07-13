using System.ComponentModel.DataAnnotations;

namespace SigmaApoyos.Application.DTOs.Expedientes;

public class CrearExpedienteDto
{
    [Required(ErrorMessage = "La identificación del estudiante es obligatoria.")]
    [StringLength(9, ErrorMessage = "La identificación no puede superar los 9 caracteres.")]
    public string IdentificacionEstudiante { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El primer apellido es obligatorio.")]
    [StringLength(100, ErrorMessage = "El primer apellido no puede superar los 100 caracteres.")]
    public string PrimerApellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El segundo apellido es obligatorio.")]
    [StringLength(100, ErrorMessage = "El segundo apellido no puede superar los 100 caracteres.")]
    public string SegundoApellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [DataType(DataType.Date)]
    public DateOnly FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El nombre del encargado es obligatorio.")]
    [StringLength(150, ErrorMessage = "El nombre del encargado no puede superar los 150 caracteres.")]
    public string NombreEncargado { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono del encargado es obligatorio.")]
    [StringLength(30, ErrorMessage = "El teléfono del encargado no puede superar los 30 caracteres.")]
    public string TelefonoEncargado { get; set; } = string.Empty;

    [Required(ErrorMessage = "Las observaciones son obligatorias.")]
    [StringLength(1000, ErrorMessage = "Las observaciones no pueden superar los 1000 caracteres.")]
    public string Observaciones { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de adecuación válido.")]
    public int IdTipoAdecuacion { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado válido.")]
    public int IdEstado { get; set; }
}
