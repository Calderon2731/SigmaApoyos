using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SigmaApoyos.Application.DTOs.Usuarios;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IActualizarUsuarioService;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerCatalogosUsuarioService;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuarioPorIdService;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuariosService;
using SigmaApoyos.Infrastructure.Identity;

namespace SigmaApoyos.UI.Controllers;

[Authorize(Roles = IdentityRoles.UsuariosAdministracion)]
public class UsuarioController : Controller
{
    private readonly IObtenerUsuariosService _obtenerUsuariosService;
    private readonly IObtenerUsuarioPorIdService _obtenerUsuarioPorIdService;
    private readonly IActualizarUsuarioService _actualizarUsuarioService;
    private readonly IObtenerCatalogosUsuarioService _obtenerCatalogosUsuarioService;

    public UsuarioController(
        IObtenerUsuariosService obtenerUsuariosService,
        IObtenerUsuarioPorIdService obtenerUsuarioPorIdService,
        IActualizarUsuarioService actualizarUsuarioService,
        IObtenerCatalogosUsuarioService obtenerCatalogosUsuarioService)
    {
        _obtenerUsuariosService = obtenerUsuariosService;
        _obtenerUsuarioPorIdService = obtenerUsuarioPorIdService;
        _actualizarUsuarioService = actualizarUsuarioService;
        _obtenerCatalogosUsuarioService = obtenerCatalogosUsuarioService;
    }

    public async Task<IActionResult> ObtenerUsuarios()
    {
        var usuarios = await _obtenerUsuariosService.ObtenerTodosAsync();
        return View(usuarios);
    }

    public async Task<IActionResult> EditarUsuario(string id)
    {
        var usuario = await _obtenerUsuarioPorIdService.ObtenerParaEditarAsync(id);

        if (usuario == null)
        {
            return NotFound();
        }

        await CargarCombosAsync();
        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarUsuario(UpdateUsuarioDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await CargarCombosAsync();
                return View(dto);
            }

            var usuarioActualId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var seActualizo = await _actualizarUsuarioService.ActualizarAsync(dto, usuarioActualId);

            if (seActualizo)
            {
                TempData["SuccessMessage"] = "Usuario actualizado correctamente.";
                return RedirectToAction(nameof(ObtenerUsuarios));
            }

            ModelState.AddModelError(string.Empty, "No se pudo actualizar el usuario. Verifica que no estés quitándote tu propio rol de administrador o dejándote inactivo.");
            await CargarCombosAsync();
            return View(dto);
        }
        catch
        {
            await CargarCombosAsync();
            return View(dto);
        }
    }

    private async Task CargarCombosAsync()
    {
        var roles = await _obtenerCatalogosUsuarioService.ObtenerRolesAsync();
        var estados = await _obtenerCatalogosUsuarioService.ObtenerEstadosAsync();

        ViewBag.Roles = roles
            .Select(x => new SelectListItem(x, x))
            .ToList();

        ViewBag.Estados = estados
            .Select(x => new SelectListItem(x.Nombre, x.Id.ToString()))
            .ToList();
    }
}
