using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Services.Correo.INotificarCoordinadorService;
using SigmaApoyos.Application.Interfaces.Services.Documento.ICrearDocumentoService;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Services.Documentos.CrearDocumento;

public sealed class CrearDocumento : ICrearDocumentoService
{
    private readonly ICrearDocumentoRepository _crearDocumentoRepository;
    private readonly IObtenerExpedientePorIdRepository _obtenerExpedientePorIdRepository;
    private readonly INotificarCoordinadorService _notificarCoordinadorService;

    public CrearDocumento(
        ICrearDocumentoRepository crearDocumentoRepository,
        IObtenerExpedientePorIdRepository obtenerExpedientePorIdRepository,
        INotificarCoordinadorService notificarCoordinadorService)
    {
        _crearDocumentoRepository = crearDocumentoRepository;
        _obtenerExpedientePorIdRepository = obtenerExpedientePorIdRepository;
        _notificarCoordinadorService = notificarCoordinadorService;
    }

    public async Task<bool> CrearAsync(CrearDocumentoDto dto, CancellationToken cancellationToken = default)
    {
        var expediente = await _obtenerExpedientePorIdRepository.ObtenerPorIdAsync(dto.IdentificacionEstudiante, cancellationToken);

        if (expediente == null)
        {
            return false;
        }

        var documento = new Documento
        {
            IdentificacionEstudiante = dto.IdentificacionEstudiante,
            TipoDocumentoId = dto.TipoDocumentoId,
            UsuarioId = dto.UsuarioId,
            Consecutivo = dto.Consecutivo,
            RutaArchivo = dto.RutaArchivo,
            FechaSubida = DateTime.Now,
            IdEstado = dto.IdEstado
        };

        await _crearDocumentoRepository.CrearAsync(documento, cancellationToken);
        await _notificarCoordinadorService.NotificarNuevoDocumentoAsync(dto, cancellationToken);
        return true;
    }
}
