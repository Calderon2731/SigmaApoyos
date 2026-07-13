using System.Security.Claims;
using SigmaApoyos.Infrastructure.Identity;

namespace SigmaApoyos.UI.Security;

public static class ClaimsPrincipalExtensions
{
    public static bool PuedeLeerExpedientes(this ClaimsPrincipal user)
    {
        return TieneUnoDeEstosRoles(user, IdentityRoles.RolesDelSistema) || user.IsInRole(IdentityRoles.LegacyAdministrador);
    }

    public static bool PuedeCrearExpedientes(this ClaimsPrincipal user)
    {
        return user.IsInRole(IdentityRoles.Docente)
            || user.IsInRole(IdentityRoles.Biblioteca)
            || user.IsInRole(IdentityRoles.Guia)
            || user.IsInRole(IdentityRoles.CoordinadorAcademico)
            || user.IsInRole(IdentityRoles.LegacyAdministrador);
    }

    public static bool PuedeModificarExpedientes(this ClaimsPrincipal user)
    {
        return user.IsInRole(IdentityRoles.Guia)
            || user.IsInRole(IdentityRoles.CoordinadorAcademico)
            || user.IsInRole(IdentityRoles.LegacyAdministrador);
    }

    public static bool PuedeEliminarExpedientes(this ClaimsPrincipal user)
    {
        return user.IsInRole(IdentityRoles.CoordinadorAcademico)
            || user.IsInRole(IdentityRoles.LegacyAdministrador);
    }

    public static bool PuedeLeerDocumentos(this ClaimsPrincipal user)
    {
        return PuedeLeerExpedientes(user);
    }

    public static bool PuedeCrearDocumentos(this ClaimsPrincipal user)
    {
        return PuedeCrearExpedientes(user);
    }

    public static bool PuedeModificarDocumentos(this ClaimsPrincipal user)
    {
        return PuedeModificarExpedientes(user);
    }

    public static bool PuedeEliminarDocumentos(this ClaimsPrincipal user)
    {
        return PuedeEliminarExpedientes(user);
    }

    public static bool PuedeGestionarUsuarios(this ClaimsPrincipal user)
    {
        return user.IsInRole(IdentityRoles.CoordinadorAcademico)
            || user.IsInRole(IdentityRoles.LegacyAdministrador);
    }

    private static bool TieneUnoDeEstosRoles(ClaimsPrincipal user, IEnumerable<string> roles)
    {
        return roles.Any(user.IsInRole);
    }
}
