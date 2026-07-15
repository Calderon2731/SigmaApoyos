namespace SigmaApoyos.Infrastructure.Identity;

public static class IdentityRoles
{
    public const string LegacyAdministrador = "Administrador";

    public const string Docente = "Docente";
    public const string Directora = "Directora";
    public const string Subdirector = "Subdirector";
    public const string Auxiliar = "Auxiliar";
    public const string Biblioteca = "Biblioteca";
    public const string Oficinistas = "Oficinistas";
    public const string Guia = "Guía";
    public const string Orientacion = "Orientación";
    public const string CoordinadorAcademico = "Coordinador académico";

    public static readonly string[] RolesDelSistema =
    {
        Docente,
        Directora,
        Subdirector,
        Auxiliar,
        Biblioteca,
        Oficinistas,
        Guia,
        Orientacion,
        CoordinadorAcademico
    };

    public const string SoloLectura = $"{Directora},{Subdirector},{Auxiliar},{Oficinistas},{Orientacion}";
    public const string RealizaExpedientes = $"{Docente},{Biblioteca}";
    public const string PuedeModificar = $"{Guia}";
    public const string AdministracionTotal = $"{CoordinadorAcademico},{LegacyAdministrador}";

    public const string ExpedientesLectura = $"{SoloLectura},{RealizaExpedientes},{PuedeModificar},{AdministracionTotal}";
    public const string ExpedientesCrear = $"{RealizaExpedientes},{PuedeModificar},{AdministracionTotal}";
    public const string ExpedientesModificar = $"{PuedeModificar},{AdministracionTotal}";
    public const string ExpedientesEliminar = AdministracionTotal;

    public const string DocumentosLectura = ExpedientesLectura;
    public const string DocumentosCrear = ExpedientesCrear;
    public const string DocumentosModificar = ExpedientesModificar;
    public const string DocumentosEliminar = AdministracionTotal;

    public const string UsuariosAdministracion = AdministracionTotal;
    public const string CatalogosAdministracion = AdministracionTotal;
    public const string AuditoriaAdministracion = AdministracionTotal;

    public static bool EsRolVisible(string? roleName)
    {
        return RolesDelSistema.Contains(roleName);
    }

    public static bool EsAdministradorTotal(string? roleName)
    {
        return roleName == CoordinadorAcademico || roleName == LegacyAdministrador;
    }
}
