using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IActualizarExpedienteService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.ICrearExpedienteService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IEliminarExpedienteService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedientePorIdService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedienteService;
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

        public ExpedienteController(
            IObtenerExpedienteService obtenerExpedientesService,
            IObtenerExpedientePorIdService obtenerExpedientePorIdService,
            IActualizarExpedienteService actualizarExpedienteService,
            ICrearExpedienteService crearExpedienteService,
            IEliminarExpedienteService eliminarExpedienteService)
        {
            _obtenerExpedientesService = obtenerExpedientesService;
            _obtenerExpedientePorIdService = obtenerExpedientePorIdService;
            _actualizarExpedienteService = actualizarExpedienteService;
            _crearExpedienteService = crearExpedienteService;
            _eliminarExpedienteService = eliminarExpedienteService;
        }

        public async Task<IActionResult> ObtenerExpedientes(string identificacion)
        {
            if (!string.IsNullOrWhiteSpace(identificacion))
            {
                var expediente = await _obtenerExpedientePorIdService.ObtenerPorIdentificacionAsync(identificacion);

                var lista = new List<ExpedienteDto>();

                if (expediente != null)
                    lista.Add(expediente);

                return View(lista);
            }

            var expedientes = await _obtenerExpedientesService.ObtenerTodosAsync();
            return View(expedientes);
        }

        public async Task<IActionResult> DetallesExpediente(string identificacion)
        {
            var expediente = await _obtenerExpedientePorIdService.ObtenerPorIdentificacionAsync(identificacion);

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
