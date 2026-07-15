using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigmaApoyos.Application.Interfaces.Services.Auditoria.IObtenerAuditoriaPorIdService;
using SigmaApoyos.Application.Interfaces.Services.Auditoria.IObtenerAuditoriasService;
using SigmaApoyos.Infrastructure.Identity;

namespace SigmaApoyos.UI.Controllers;

[Authorize(Roles = IdentityRoles.AuditoriaAdministracion)]
public class AuditoriaController : Controller
{
    private readonly IObtenerAuditoriasService _obtenerAuditoriasService;
    private readonly IObtenerAuditoriaPorIdService _obtenerAuditoriaPorIdService;

    public AuditoriaController(
        IObtenerAuditoriasService obtenerAuditoriasService,
        IObtenerAuditoriaPorIdService obtenerAuditoriaPorIdService)
    {
        _obtenerAuditoriasService = obtenerAuditoriasService;
        _obtenerAuditoriaPorIdService = obtenerAuditoriaPorIdService;
    }

    public async Task<IActionResult> ObtenerAuditorias()
    {
        return View(await _obtenerAuditoriasService.ObtenerTodosAsync());
    }

    public async Task<IActionResult> DetallesAuditoria(long id)
    {
        var auditoria = await _obtenerAuditoriaPorIdService.ObtenerPorIdAsync(id);
        return auditoria == null ? NotFound() : View(auditoria);
    }
}
