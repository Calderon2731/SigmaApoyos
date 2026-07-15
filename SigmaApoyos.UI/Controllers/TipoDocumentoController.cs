using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigmaApoyos.Application.DTOs.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IActualizarTipoDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.TipoDocumento.ICrearTipoDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IEliminarTipoDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IObtenerTipoDocumentoPorIdService;
using SigmaApoyos.Application.Interfaces.Services.TipoDocumento.IObtenerTipoDocumentoService;
using SigmaApoyos.Infrastructure.Identity;

namespace SigmaApoyos.UI.Controllers;

[Authorize(Roles = IdentityRoles.CatalogosAdministracion)]
public class TipoDocumentoController : Controller
{
    private readonly IObtenerTipoDocumentoService _obtenerService;
    private readonly IObtenerTipoDocumentoPorIdService _obtenerPorIdService;
    private readonly ICrearTipoDocumentoService _crearService;
    private readonly IActualizarTipoDocumentoService _actualizarService;
    private readonly IEliminarTipoDocumentoService _eliminarService;

    public TipoDocumentoController(IObtenerTipoDocumentoService obtenerService,
        IObtenerTipoDocumentoPorIdService obtenerPorIdService, ICrearTipoDocumentoService crearService,
        IActualizarTipoDocumentoService actualizarService, IEliminarTipoDocumentoService eliminarService)
    {
        _obtenerService = obtenerService;
        _obtenerPorIdService = obtenerPorIdService;
        _crearService = crearService;
        _actualizarService = actualizarService;
        _eliminarService = eliminarService;
    }

    public async Task<IActionResult> ObtenerTiposDocumento() => View(await _obtenerService.ObtenerTodosAsync());
    public IActionResult CrearTipoDocumento() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearTipoDocumento(CrearTipoDocumentoDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        try
        {
            await _crearService.CrearAsync(dto);
            TempData["SuccessMessage"] = "Tipo de documento creado correctamente.";
            return RedirectToAction(nameof(ObtenerTiposDocumento));
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "No se pudo crear el tipo de documento.");
            return View(dto);
        }
    }

    public async Task<IActionResult> EditarTipoDocumento(int id)
    {
        var dto = await _actualizarService.ObtenerParaEditarAsync(id);
        return dto == null ? NotFound() : View(dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarTipoDocumento(UpdateTipoDocumentoDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        try
        {
            if (!await _actualizarService.ActualizarAsync(dto)) return NotFound();
            TempData["SuccessMessage"] = "Tipo de documento actualizado correctamente.";
            return RedirectToAction(nameof(ObtenerTiposDocumento));
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el tipo de documento.");
            return View(dto);
        }
    }

    public async Task<IActionResult> EliminarTipoDocumento(int id)
    {
        var dto = await _obtenerPorIdService.ObtenerPorIdAsync(id);
        return dto == null ? NotFound() : View(dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmarEliminarTipoDocumento(int id)
    {
        try
        {
            await _eliminarService.EliminarAsync(id);
            TempData["SuccessMessage"] = "Tipo de documento eliminado correctamente.";
        }
        catch
        {
            TempData["ErrorMessage"] = "No se puede eliminar el tipo de documento porque está siendo utilizado.";
        }
        return RedirectToAction(nameof(ObtenerTiposDocumento));
    }
}
