namespace SigmaApoyos.Application.DTOs.Expedientes;

using System.ComponentModel.DataAnnotations;

public class ExpedienteDto
{
    [Display(Name = "Identificación")]
    [Required]
    public string IdentificacionEstudiante { get; set; } = string.Empty;
    
    [Display(Name = "Nombre")]
    [Required]  
    public string Nombre { get; set; } = string.Empty;
    
    [Display(Name = "Primer Apellido")]
    [Required]
    public string PrimerApellido { get; set; } = string.Empty;

    [Display(Name = "Segundo Apellido")]
    [Required]
    public string SegundoApellido { get; set; } = string.Empty;

    [Display(Name = "Fecha de Nacimiento")]
    [Required]
    public DateTime FechaNacimiento { get; set; }

    [Display(Name = " Encargado")]
    [Required]
    public string NombreEncargado { get; set; } = string.Empty;
    [Display(Name = "Teléfono Encargado")]
    public string TelefonoEncargado { get; set; } = string.Empty;

    [Display(Name = "Observaciones")]
    [Required]
    public string Observaciones { get; set; } = string.Empty;

    [Display(Name = "Tipo Adecuación")]
    [Required]
    public int IdTipoAdecuacion { get; set; }

    [Display(Name = "Estado")]
    [Required]
    public int IdEstado { get; set; }
}
