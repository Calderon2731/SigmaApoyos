using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SigmaApoyos.Application.Interfaces;
using SigmaApoyos.Application.Interfaces.Repositories;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Auditorias;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IActualizarExpedienteRepository;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IEliminarExpedienteRepository;
using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IActualizarUsuarioService;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerCatalogosUsuarioService;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuarioPorIdService;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuariosService;
using SigmaApoyos.Infrastructure.Identity.Services;
using SigmaApoyos.Infrastructure.Persistence;
using SigmaApoyos.Infrastructure.Repositories;
using SigmaApoyos.Infrastructure.Repositories.Documentos.ActualizarDocumentoRepository;
using SigmaApoyos.Infrastructure.Repositories.Auditorias.ObtenerAuditoriaPorIdRepository;
using SigmaApoyos.Infrastructure.Repositories.Auditorias.ObtenerAuditoriasRepository;
using SigmaApoyos.Infrastructure.Repositories.Auditorias.RegistrarAuditoriaRepository;
using SigmaApoyos.Infrastructure.Repositories.Documentos.CrearDocumentoRepository;
using SigmaApoyos.Infrastructure.Repositories.Documentos.EliminarDocumentoRepository;
using SigmaApoyos.Infrastructure.Repositories.Documentos.ObtenerCatalogosDocumentoRepository;
using SigmaApoyos.Infrastructure.Repositories.Documentos.ObtenerDocumentoPorIdRepository;
using SigmaApoyos.Infrastructure.Repositories.Documentos.ObtenerDocumentosRepository;
using SigmaApoyos.Infrastructure.Repositories.Expedientes.ActualizarExpedienteRepository;
using SigmaApoyos.Infrastructure.Repositories.Expedientes.CrearExpedienteRepository;
using SigmaApoyos.Infrastructure.Repositories.Expedientes.EliminarExpedienteRepository;
using SigmaApoyos.Infrastructure.Repositories.Expedientes.ObtenerExpedientePorIdRepository;
using SigmaApoyos.Infrastructure.Repositories.Expedientes.ObtenerExpedientesRepository;
using SigmaApoyos.Infrastructure.Repositories.Estados.ActualizarEstadoRepository;
using SigmaApoyos.Infrastructure.Repositories.Estados.CrearEstadoRepository;
using SigmaApoyos.Infrastructure.Repositories.Estados.EliminarEstadoRepository;
using SigmaApoyos.Infrastructure.Repositories.Estados.ObtenerEstadoPorIdRepository;
using SigmaApoyos.Infrastructure.Repositories.Estados.ObtenerEstadosRepository;
using SigmaApoyos.Infrastructure.Repositories.TiposAdecuacion.ActualizarTipoAdecuacionRepository;
using SigmaApoyos.Infrastructure.Repositories.TiposAdecuacion.CrearTipoAdecuacionRepository;
using SigmaApoyos.Infrastructure.Repositories.TiposAdecuacion.EliminarTipoAdecuacionRepository;
using SigmaApoyos.Infrastructure.Repositories.TiposAdecuacion.ObtenerTipoAdecuacionPorIdRepository;
using SigmaApoyos.Infrastructure.Repositories.TiposAdecuacion.ObtenerTiposAdecuacionRepository;
using SigmaApoyos.Infrastructure.Repositories.TiposDocumento.ActualizarTipoDocumentoRepository;
using SigmaApoyos.Infrastructure.Repositories.TiposDocumento.CrearTipoDocumentoRepository;
using SigmaApoyos.Infrastructure.Repositories.TiposDocumento.EliminarTipoDocumentoRepository;
using SigmaApoyos.Infrastructure.Repositories.TiposDocumento.ObtenerTipoDocumentoPorIdRepository;
using SigmaApoyos.Infrastructure.Repositories.TiposDocumento.ObtenerTiposDocumentoRepository;

namespace SigmaApoyos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddHttpContextAccessor();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IObtenerExpedientesRepository, ObtenerExpedientesRepository>();
        services.AddScoped<IObtenerExpedientePorIdRepository, ObtenerExpedientePorIdRepository>();
        services.AddScoped<ICrearExpedienteRepository, CrearExpedienteRepository>();
        services.AddScoped<IActualizarExpedienteRepository, ActualizarExpedienteRepository>();
        services.AddScoped<IEliminarExpedienteRepository, EliminarExpedienteRepository>();
        services.AddScoped<IObtenerDocumentosRepository, ObtenerDocumentosRepository>();
        services.AddScoped<IObtenerDocumentoPorIdRepository, ObtenerDocumentoPorIdRepository>();
        services.AddScoped<ICrearDocumentoRepository, CrearDocumentoRepository>();
        services.AddScoped<IActualizarDocumentoRepository, ActualizarDocumentoRepository>();
        services.AddScoped<IEliminarDocumentoRepository, EliminarDocumentoRepository>();
        services.AddScoped<IObtenerCatalogosDocumentoRepository, ObtenerCatalogosDocumentoRepository>();
        services.AddScoped<IDocumentoRepository, DocumentoRepository>();
        services.AddScoped<IObtenerUsuariosService, ObtenerUsuariosService>();
        services.AddScoped<IObtenerUsuarioPorIdService, ObtenerUsuarioPorIdService>();
        services.AddScoped<IActualizarUsuarioService, ActualizarUsuarioService>();
        services.AddScoped<IObtenerCatalogosUsuarioService, ObtenerCatalogosUsuarioService>();
        services.AddScoped<IObtenerEstadosRepository, ObtenerEstadosRepository>();
        services.AddScoped<IObtenerEstadoPorIdRepository, ObtenerEstadoPorIdRepository>();
        services.AddScoped<ICrearEstadoRepository, CrearEstadoRepository>();
        services.AddScoped<IActualizarEstadoRepository, ActualizarEstadoRepository>();
        services.AddScoped<IEliminarEstadoRepository, EliminarEstadoRepository>();
        services.AddScoped<IObtenerTiposAdecuacionRepository, ObtenerTiposAdecuacionRepository>();
        services.AddScoped<IObtenerTipoAdecuacionPorIdRepository, ObtenerTipoAdecuacionPorIdRepository>();
        services.AddScoped<ICrearTipoAdecuacionRepository, CrearTipoAdecuacionRepository>();
        services.AddScoped<IActualizarTipoAdecuacionRepository, ActualizarTipoAdecuacionRepository>();
        services.AddScoped<IEliminarTipoAdecuacionRepository, EliminarTipoAdecuacionRepository>();
        services.AddScoped<IObtenerTiposDocumentoRepository, ObtenerTiposDocumentoRepository>();
        services.AddScoped<IObtenerTipoDocumentoPorIdRepository, ObtenerTipoDocumentoPorIdRepository>();
        services.AddScoped<ICrearTipoDocumentoRepository, CrearTipoDocumentoRepository>();
        services.AddScoped<IActualizarTipoDocumentoRepository, ActualizarTipoDocumentoRepository>();
        services.AddScoped<IEliminarTipoDocumentoRepository, EliminarTipoDocumentoRepository>();
        services.AddScoped<IObtenerAuditoriasRepository, ObtenerAuditoriasRepository>();
        services.AddScoped<IObtenerAuditoriaPorIdRepository, ObtenerAuditoriaPorIdRepository>();
        services.AddScoped<IRegistrarAuditoriaRepository, RegistrarAuditoriaRepository>();

        return services;
    }
}
