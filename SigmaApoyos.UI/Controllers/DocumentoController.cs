using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.DTOs.Auditorias;
using SigmaApoyos.Application.Interfaces.Services.Auditoria.IRegistrarAuditoriaService;
using SigmaApoyos.Application.Interfaces.Services.Documento.IActualizarDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.Documento.ICrearDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.Documento.IEliminarDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerCatalogosDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerDocumentoPorIdService;
using SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerDocumentoService;
using SigmaApoyos.Infrastructure.Identity;

namespace SigmaApoyos.UI.Controllers;

[Authorize(Roles = IdentityRoles.DocumentosLectura)]
public class DocumentoController : Controller
{
    private readonly IObtenerDocumentoService _obtenerDocumentoService;
    private readonly IObtenerDocumentoPorIdService _obtenerDocumentoPorIdService;
    private readonly ICrearDocumentoService _crearDocumentoService;
    private readonly IActualizarDocumentoService _actualizarDocumentoService;
    private readonly IEliminarDocumentoService _eliminarDocumentoService;
    private readonly IObtenerCatalogosDocumentoService _obtenerCatalogosDocumentoService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IRegistrarAuditoriaService _registrarAuditoriaService;

    public DocumentoController(
        IObtenerDocumentoService obtenerDocumentoService,
        IObtenerDocumentoPorIdService obtenerDocumentoPorIdService,
        ICrearDocumentoService crearDocumentoService,
        IActualizarDocumentoService actualizarDocumentoService,
        IEliminarDocumentoService eliminarDocumentoService,
        IObtenerCatalogosDocumentoService obtenerCatalogosDocumentoService,
        IWebHostEnvironment webHostEnvironment,
        IRegistrarAuditoriaService registrarAuditoriaService)
    {
        _obtenerDocumentoService = obtenerDocumentoService;
        _obtenerDocumentoPorIdService = obtenerDocumentoPorIdService;
        _crearDocumentoService = crearDocumentoService;
        _actualizarDocumentoService = actualizarDocumentoService;
        _eliminarDocumentoService = eliminarDocumentoService;
        _obtenerCatalogosDocumentoService = obtenerCatalogosDocumentoService;
        _webHostEnvironment = webHostEnvironment;
        _registrarAuditoriaService = registrarAuditoriaService;
    }

    public async Task<IActionResult> ObtenerDocumentos(
        FiltroDocumentoDto filtro,
        CancellationToken cancellationToken)
    {
        var documentos = await _obtenerDocumentoService.ObtenerTodosAsync(filtro, cancellationToken);
        var tiposDocumento = await _obtenerCatalogosDocumentoService.ObtenerTiposDocumentoAsync(cancellationToken);
        var estados = await _obtenerCatalogosDocumentoService.ObtenerEstadosAsync(cancellationToken);

        ViewBag.Filtro = filtro;
        ViewBag.TiposDocumentoFiltro = new SelectList(
            tiposDocumento,
            "Id",
            "Nombre",
            filtro.TipoDocumentoId);
        ViewBag.EstadosFiltro = new SelectList(estados, "Id", "Nombre", filtro.IdEstado);

        return View(documentos);
    }

    public async Task<IActionResult> DetallesDocumento(int id)
    {
        var documento = await _obtenerDocumentoPorIdService.ObtenerPorIdAsync(id);

        if (documento == null)
        {
            return NotFound();
        }

        return View(documento);
    }

    [Authorize(Roles = IdentityRoles.DocumentosCrear)]
    public async Task<IActionResult> CrearDocumento(string? identificacionEstudiante)
    {
        await CargarCombosAsync();
        return View(new CrearDocumentoDto
        {
            IdentificacionEstudiante = identificacionEstudiante ?? string.Empty
        });
    }

    [Authorize(Roles = IdentityRoles.DocumentosCrear)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearDocumento(CrearDocumentoDto dto, IFormFile? archivoPdf)
    {
        try
        {
            if (archivoPdf == null || archivoPdf.Length == 0)
            {
                ModelState.AddModelError("archivoPdf", "Debe seleccionar un archivo PDF.");
            }
            else if (!EsPdfValido(archivoPdf))
            {
                ModelState.AddModelError("archivoPdf", "Solo se permiten archivos PDF.");
            }

            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                ModelState.AddModelError(string.Empty, "Debe iniciar sesión para registrar documentos.");
            }

            if (!ModelState.IsValid)
            {
                await CargarCombosAsync();
                return View(dto);
            }

            dto.UsuarioId = usuarioId!;
            dto.RutaArchivo = await GuardarArchivoAsync(archivoPdf!);

            var seCreo = await _crearDocumentoService.CrearAsync(dto);

            if (seCreo)
            {
                return RedirectToAction(nameof(ObtenerDocumentos));
            }

            EliminarArchivoSiExiste(dto.RutaArchivo);
            ModelState.AddModelError(string.Empty, "No se pudo crear el documento. Verifique la identificación del expediente.");
            await CargarCombosAsync();
            return View(dto);
        }
        catch
        {
            EliminarArchivoSiExiste(dto.RutaArchivo);
            await CargarCombosAsync();
            return View(dto);
        }
    }

    [Authorize(Roles = IdentityRoles.DocumentosModificar)]
    public async Task<IActionResult> EditarDocumento(int id)
    {
        var dto = await _actualizarDocumentoService.ObtenerParaEditarAsync(id);

        if (dto == null)
        {
            return NotFound();
        }

        await CargarCombosAsync();
        return View(dto);
    }

    [Authorize(Roles = IdentityRoles.DocumentosModificar)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarDocumento(UpdateDocumentoDto dto, IFormFile? archivoPdf)
    {
        try
        {
            var rutaAnterior = dto.RutaArchivo;

            if (archivoPdf != null)
            {
                if (!EsPdfValido(archivoPdf))
                {
                    ModelState.AddModelError("archivoPdf", "Solo se permiten archivos PDF.");
                }
                else
                {
                    dto.RutaArchivo = await GuardarArchivoAsync(archivoPdf);
                }
            }

            if (!ModelState.IsValid)
            {
                if (!string.Equals(rutaAnterior, dto.RutaArchivo, StringComparison.OrdinalIgnoreCase))
                {
                    EliminarArchivoSiExiste(dto.RutaArchivo);
                    dto.RutaArchivo = rutaAnterior;
                }

                await CargarCombosAsync();
                return View(dto);
            }

            var seEdito = await _actualizarDocumentoService.ActualizarAsync(dto);

            if (seEdito)
            {
                if (!string.Equals(rutaAnterior, dto.RutaArchivo, StringComparison.OrdinalIgnoreCase))
                {
                    EliminarArchivoSiExiste(rutaAnterior);
                }

                return RedirectToAction(nameof(ObtenerDocumentos));
            }

            if (!string.Equals(rutaAnterior, dto.RutaArchivo, StringComparison.OrdinalIgnoreCase))
            {
                EliminarArchivoSiExiste(dto.RutaArchivo);
                dto.RutaArchivo = rutaAnterior;
            }

            ModelState.AddModelError(string.Empty, "No se pudo actualizar el documento.");
            await CargarCombosAsync();
            return View(dto);
        }
        catch
        {
            await CargarCombosAsync();
            return View(dto);
        }
    }

    [Authorize(Roles = IdentityRoles.DocumentosEliminar)]
    public async Task<IActionResult> EliminarDocumento(int id)
    {
        var documento = await _obtenerDocumentoPorIdService.ObtenerPorIdAsync(id);

        if (documento == null)
        {
            return NotFound();
        }

        return View(documento);
    }

    [Authorize(Roles = IdentityRoles.DocumentosEliminar)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmarEliminarDocumento(int id)
    {
        try
        {
            var documento = await _obtenerDocumentoPorIdService.ObtenerPorIdAsync(id);

            if (documento == null)
            {
                return RedirectToAction(nameof(ObtenerDocumentos));
            }

            var seElimino = await _eliminarDocumentoService.EliminarAsync(id);

            if (seElimino)
            {
                EliminarArchivoSiExiste(documento.RutaArchivo);
                return RedirectToAction(nameof(ObtenerDocumentos));
            }

            return RedirectToAction(nameof(EliminarDocumento), new { id });
        }
        catch
        {
            return RedirectToAction(nameof(EliminarDocumento), new { id });
        }
    }

    public async Task<IActionResult> DescargarDocumento(int id)
    {
        var documento = await _obtenerDocumentoPorIdService.ObtenerPorIdAsync(id);

        if (documento == null || string.IsNullOrWhiteSpace(documento.RutaArchivo))
        {
            return NotFound();
        }

        var rutaRelativa = documento.RutaArchivo.TrimStart('/', '\\')
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var rutaFisica = Path.Combine(_webHostEnvironment.WebRootPath, rutaRelativa);

        if (!System.IO.File.Exists(rutaFisica))
        {
            return NotFound();
        }

        await _registrarAuditoriaService.RegistrarAsync(new RegistrarAuditoriaDto
        {
            UsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            UsuarioNombre = User.Identity?.Name ?? "Sistema",
            Accion = "Descargar",
            Entidad = "Documento",
            RegistroId = documento.IdDocumento.ToString(),
            DireccionIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
            Ruta = HttpContext.Request.Path.Value,
            Descripcion = $"Descarga del documento {documento.Consecutivo}"
        });

        return PhysicalFile(rutaFisica, "application/pdf", Path.GetFileName(rutaFisica));
    }

    private async Task CargarCombosAsync()
    {
        var tiposDocumento = await _obtenerCatalogosDocumentoService.ObtenerTiposDocumentoAsync();
        var estados = await _obtenerCatalogosDocumentoService.ObtenerEstadosAsync();

        ViewBag.TiposDocumento = tiposDocumento
            .Select(x => new SelectListItem(x.Nombre, x.Id.ToString()))
            .ToList();

        ViewBag.Estados = estados
            .Select(x => new SelectListItem(x.Nombre, x.Id.ToString()))
            .ToList();
    }

    private bool EsPdfValido(IFormFile archivoPdf)
    {
        var extension = Path.GetExtension(archivoPdf.FileName);
        return string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase);
    }

    private async Task<string> GuardarArchivoAsync(IFormFile archivoPdf)
    {
        var carpetaDocumentos = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "documentos");
        Directory.CreateDirectory(carpetaDocumentos);

        var nombreArchivo = $"DOC_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}.pdf";
        var rutaFisica = Path.Combine(carpetaDocumentos, nombreArchivo);

        await using var stream = new FileStream(rutaFisica, FileMode.Create);
        await archivoPdf.CopyToAsync(stream);

        return $"/uploads/documentos/{nombreArchivo}";
    }

    private void EliminarArchivoSiExiste(string? rutaArchivo)
    {
        if (string.IsNullOrWhiteSpace(rutaArchivo))
        {
            return;
        }

        var rutaRelativa = rutaArchivo.TrimStart('/', '\\')
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        var rutaFisica = Path.Combine(_webHostEnvironment.WebRootPath, rutaRelativa);

        if (System.IO.File.Exists(rutaFisica))
        {
            System.IO.File.Delete(rutaFisica);
        }
    }
}
