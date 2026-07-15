using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigmaApoyos.Application.DTOs.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IActualizarTipoAdecuacionService;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.ICrearTipoAdecuacionService;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IEliminarTipoAdecuacionService;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IObtenerTipoAdecuacionPorIdService;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IObtenerTipoAdecuacionService;
using SigmaApoyos.Infrastructure.Identity;

namespace SigmaApoyos.UI.Controllers;

[Authorize(Roles = IdentityRoles.CatalogosAdministracion)]
public class TipoAdecuacionController : Controller
{
    private readonly IObtenerTipoAdecuacionService _obtenerService;
    private readonly IObtenerTipoAdecuacionPorIdService _obtenerPorIdService;
    private readonly ICrearTipoAdecuacionService _crearService;
    private readonly IActualizarTipoAdecuacionService _actualizarService;
    private readonly IEliminarTipoAdecuacionService _eliminarService;

    public TipoAdecuacionController(IObtenerTipoAdecuacionService obtenerService,
        IObtenerTipoAdecuacionPorIdService obtenerPorIdService, ICrearTipoAdecuacionService crearService,
        IActualizarTipoAdecuacionService actualizarService, IEliminarTipoAdecuacionService eliminarService)
    {
        _obtenerService = obtenerService;
        _obtenerPorIdService = obtenerPorIdService;
        _crearService = crearService;
        _actualizarService = actualizarService;
        _eliminarService = eliminarService;
    }

    public async Task<IActionResult> ObtenerTiposAdecuacion() => View(await _obtenerService.ObtenerTodosAsync());
    public IActionResult CrearTipoAdecuacion() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearTipoAdecuacion(CrearTipoAdecuacionDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        try
        {
            await _crearService.CrearAsync(dto);
            TempData["SuccessMessage"] = "Tipo de adecuación creado correctamente.";
            return RedirectToAction(nameof(ObtenerTiposAdecuacion));
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el tipo de adecuación.");
            return View(dto);
        }
    }

    public async Task<IActionResult> EditarTipoAdecuacion(int id)
    {
        var dto = await _actualizarService.ObtenerParaEditarAsync(id);
        return dto == null ? NotFound() : View(dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarTipoAdecuacion(UpdateTipoAdecuacionDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        try
        {
            if (!await _actualizarService.ActualizarAsync(dto)) return NotFound();
            TempData["SuccessMessage"] = "Tipo de adecuación actualizado correctamente.";
            return RedirectToAction(nameof(ObtenerTiposAdecuacion));
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el tipo de adecuación.");
            return View(dto);
        }
    }

    public async Task<IActionResult> EliminarTipoAdecuacion(int id)
    {
        var dto = await _obtenerPorIdService.ObtenerPorIdAsync(id);
        return dto == null ? NotFound() : View(dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmarEliminarTipoAdecuacion(int id)
    {
        try
        {
            await _eliminarService.EliminarAsync(id);
            TempData["SuccessMessage"] = "Tipo de adecuación eliminado correctamente.";
        }
        catch
        {
            TempData["ErrorMessage"] = "No se puede eliminar el tipo de adecuación porque está siendo utilizado.";
        }
        return RedirectToAction(nameof(ObtenerTiposAdecuacion));
    }
}
