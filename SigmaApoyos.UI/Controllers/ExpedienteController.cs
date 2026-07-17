using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IActualizarExpedienteService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.ICrearExpedienteService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IEliminarExpedienteService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedientePorIdService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedienteIntegralService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedienteService;
using SigmaApoyos.Application.Interfaces.Services.Estado.IObtenerEstadoService;
using SigmaApoyos.Application.Interfaces.Services.TipoAdecuacion.IObtenerTipoAdecuacionService;
using SigmaApoyos.Infrastructure.Identity;

namespace SigmaApoyos.UI.Controllers
{
    [Authorize(Roles = IdentityRoles.ExpedientesLectura)]
    public class ExpedienteController : Controller
    {
        private readonly IObtenerExpedienteService _obtenerExpedientesService;
        private readonly IObtenerExpedientePorIdService _obtenerExpedientePorIdService;
        private readonly IActualizarExpedienteService _actualizarExpedienteService;
        private readonly ICrearExpedienteService _crearExpedienteService;
        private readonly IEliminarExpedienteService _eliminarExpedienteService;
        private readonly IObtenerExpedienteIntegralService _obtenerExpedienteIntegralService;
        private readonly IObtenerEstadoService _obtenerEstadoService;
        private readonly IObtenerTipoAdecuacionService _obtenerTipoAdecuacionService;

        public ExpedienteController(
            IObtenerExpedienteService obtenerExpedientesService,
            IObtenerExpedientePorIdService obtenerExpedientePorIdService,
            IActualizarExpedienteService actualizarExpedienteService,
            ICrearExpedienteService crearExpedienteService,
            IEliminarExpedienteService eliminarExpedienteService,
            IObtenerExpedienteIntegralService obtenerExpedienteIntegralService,
            IObtenerEstadoService obtenerEstadoService,
            IObtenerTipoAdecuacionService obtenerTipoAdecuacionService)
        {
            _obtenerExpedientesService = obtenerExpedientesService;
            _obtenerExpedientePorIdService = obtenerExpedientePorIdService;
            _actualizarExpedienteService = actualizarExpedienteService;
            _crearExpedienteService = crearExpedienteService;
            _eliminarExpedienteService = eliminarExpedienteService;
            _obtenerExpedienteIntegralService = obtenerExpedienteIntegralService;
            _obtenerEstadoService = obtenerEstadoService;
            _obtenerTipoAdecuacionService = obtenerTipoAdecuacionService;
        }

        public async Task<IActionResult> ObtenerExpedientes(
            FiltroExpedienteDto filtro,
            CancellationToken cancellationToken)
        {
            var expedientes = await _obtenerExpedientesService.ObtenerTodosAsync(filtro, cancellationToken);
            var estados = await _obtenerEstadoService.ObtenerTodosAsync(cancellationToken);
            var tiposAdecuacion = await _obtenerTipoAdecuacionService.ObtenerTodosAsync(cancellationToken);

            ViewBag.Filtro = filtro;
            ViewBag.Estados = new SelectList(estados, "IdEstado", "Nombre", filtro.IdEstado);
            ViewBag.TiposAdecuacion = new SelectList(
                tiposAdecuacion,
                "IdTipoAdecuacion",
                "Nombre",
                filtro.IdTipoAdecuacion);

            return View(expedientes);
        }

        public async Task<IActionResult> DetallesExpediente(string identificacion)
        {
            var expediente = await _obtenerExpedienteIntegralService.ObtenerAsync(identificacion);

            if (expediente == null)
            {
                return NotFound();
            }

            return View(expediente);
        }

        [Authorize(Roles = IdentityRoles.ExpedientesCrear)]
        public IActionResult CrearExpediente()
        {
            return View();
        }

        [Authorize(Roles = IdentityRoles.ExpedientesCrear)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearExpediente(CrearExpedienteDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                bool seCreo = await _crearExpedienteService.CrearAsync(dto);

                if (seCreo)
                {
                    return RedirectToAction(nameof(ObtenerExpedientes));
                }

                return View(dto);
            }
            catch
            {
                return View(dto);
            }
        }

        [Authorize(Roles = IdentityRoles.ExpedientesModificar)]
        public async Task<IActionResult> EditarExpediente(string identificacion)
        {
            var dto = await _actualizarExpedienteService.ObtenerParaEditarAsync(identificacion);

            if (dto == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        [Authorize(Roles = IdentityRoles.ExpedientesModificar)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarExpediente(UpdateExpedienteDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                bool seEdito = await _actualizarExpedienteService.ActualizarAsync(dto);

                if (seEdito)
                {
                    return RedirectToAction(nameof(ObtenerExpedientes));
                }

                return View(dto);
            }
            catch
            {
                return View(dto);
            }
        }

        [Authorize(Roles = IdentityRoles.ExpedientesEliminar)]
        public async Task<IActionResult> EliminarExpediente(string identificacion)
        {
            var expediente = await _obtenerExpedientePorIdService.ObtenerPorIdentificacionAsync(identificacion);

            if (expediente == null)
            {
                return NotFound();
            }

            return View(expediente);
        }

        [Authorize(Roles = IdentityRoles.ExpedientesEliminar)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminarExpediente(string identificacion)
        {
            try
            {
                bool seElimino = await _eliminarExpedienteService.EliminarAsync(identificacion);

                if (seElimino)
                {
                    return RedirectToAction(nameof(ObtenerExpedientes));
                }

                return RedirectToAction(nameof(EliminarExpediente), new { identificacion });
            }
            catch
            {
                return RedirectToAction(nameof(EliminarExpediente), new { identificacion });
            }
        }
    }
}
