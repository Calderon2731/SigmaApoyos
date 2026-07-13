using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SigmaApoyos.Application.Interfaces;
using SigmaApoyos.Application.Interfaces.Repositories;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IActualizarExpedienteRepository;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IEliminarExpedienteRepository;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IActualizarUsuarioService;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerCatalogosUsuarioService;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuarioPorIdService;
using SigmaApoyos.Application.Interfaces.Services.Usuario.IObtenerUsuariosService;
using SigmaApoyos.Infrastructure.Identity.Services;
using SigmaApoyos.Infrastructure.Persistence;
using SigmaApoyos.Infrastructure.Repositories;
using SigmaApoyos.Infrastructure.Repositories.Documentos.ActualizarDocumentoRepository;
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

namespace SigmaApoyos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

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

        return services;
    }
}
