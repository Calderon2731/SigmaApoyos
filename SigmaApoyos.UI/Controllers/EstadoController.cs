using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigmaApoyos.Application.DTOs.Estados;
using SigmaApoyos.Application.Interfaces.Services.Estado.IActualizarEstadoService;
using SigmaApoyos.Application.Interfaces.Services.Estado.ICrearEstadoService;
using SigmaApoyos.Application.Interfaces.Services.Estado.IEliminarEstadoService;
using SigmaApoyos.Application.Interfaces.Services.Estado.IObtenerEstadoPorIdService;
using SigmaApoyos.Application.Interfaces.Services.Estado.IObtenerEstadoService;
using SigmaApoyos.Infrastructure.Identity;

namespace SigmaApoyos.UI.Controllers;

[Authorize(Roles = IdentityRoles.CatalogosAdministracion)]
public class EstadoController : Controller
{
    private readonly IObtenerEstadoService _obtenerService;
    private readonly IObtenerEstadoPorIdService _obtenerPorIdService;
    private readonly ICrearEstadoService _crearService;
    private readonly IActualizarEstadoService _actualizarService;
    private readonly IEliminarEstadoService _eliminarService;

    public EstadoController(IObtenerEstadoService obtenerService, IObtenerEstadoPorIdService obtenerPorIdService,
        ICrearEstadoService crearService, IActualizarEstadoService actualizarService, IEliminarEstadoService eliminarService)
    {
        _obtenerService = obtenerService;
        _obtenerPorIdService = obtenerPorIdService;
        _crearService = crearService;
        _actualizarService = actualizarService;
        _eliminarService = eliminarService;
    }

    public async Task<IActionResult> ObtenerEstados() => View(await _obtenerService.ObtenerTodosAsync());

    public IActionResult CrearEstado() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearEstado(CrearEstadoDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        try
        {
            await _crearService.CrearAsync(dto);
            TempData["SuccessMessage"] = "Estado creado correctamente.";
            return RedirectToAction(nameof(ObtenerEstados));
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el estado.");
            return View(dto);
        }
    }

    public async Task<IActionResult> EditarEstado(int id)
    {
        var dto = await _actualizarService.ObtenerParaEditarAsync(id);
        return dto == null ? NotFound() : View(dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarEstado(UpdateEstadoDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        try
        {
            if (!await _actualizarService.ActualizarAsync(dto)) return NotFound();
            TempData["SuccessMessage"] = "Estado actualizado correctamente.";
            return RedirectToAction(nameof(ObtenerEstados));
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el estado.");
            return View(dto);
        }
    }

    public async Task<IActionResult> EliminarEstado(int id)
    {
        var dto = await _obtenerPorIdService.ObtenerPorIdAsync(id);
        return dto == null ? NotFound() : View(dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmarEliminarEstado(int id)
    {
        try
        {
            await _eliminarService.EliminarAsync(id);
            TempData["SuccessMessage"] = "Estado eliminado correctamente.";
        }
        catch
        {
            TempData["ErrorMessage"] = "No se puede eliminar el estado porque está siendo utilizado.";
        }
        return RedirectToAction(nameof(ObtenerEstados));
    }
}
