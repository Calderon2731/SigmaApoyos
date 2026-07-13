using Microsoft.Extensions.DependencyInjection;
using SigmaApoyos.Application.Interfaces.Services.Documento.IActualizarDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.Documento.ICrearDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.Documento.IEliminarDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerCatalogosDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerDocumentoPorIdService;
using SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerDocumentoService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IActualizarExpedienteService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.ICrearExpedienteService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IEliminarExpedienteService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedientePorIdService;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedienteService;
using SigmaApoyos.Application.Services.Documentos.ActualizarDocumento;
using SigmaApoyos.Application.Services.Documentos.CrearDocumento;
using SigmaApoyos.Application.Services.Documentos.EliminarDocumento;
using SigmaApoyos.Application.Services.Documentos.ObtenerCatalogosDocumento;
using SigmaApoyos.Application.Services.Documentos.ObtenerDocumentoPorId;
using SigmaApoyos.Application.Services.Documentos.ObtenerDocumentos;
using SigmaApoyos.Application.Services.Expedientes.ActualizarExpediente;
using SigmaApoyos.Application.Services.Expedientes.CrearExpediente;
using SigmaApoyos.Application.Services.Expedientes.EliminarExpediente;
using SigmaApoyos.Application.Services.Expedientes.ObtenerExpedientePorIdentificacion;
using SigmaApoyos.Application.Services.Expedientes.ObtenerExpedientes;

namespace SigmaApoyos.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IObtenerExpedienteService, ObtenerExpedientes>();
            services.AddScoped<IObtenerExpedientePorIdService, ObtenerExpedientePorId>();
            services.AddScoped<IActualizarExpedienteService, ActualizarExpediente>();
            services.AddScoped<ICrearExpedienteService, CrearExpediente>();
            services.AddScoped<IEliminarExpedienteService, EliminarExpediente>();
            services.AddScoped<IObtenerDocumentoService, ObtenerDocumentos>();
            services.AddScoped<IObtenerDocumentoPorIdService, ObtenerDocumentoPorId>();
            services.AddScoped<ICrearDocumentoService, CrearDocumento>();
            services.AddScoped<IActualizarDocumentoService, ActualizarDocumento>();
            services.AddScoped<IEliminarDocumentoService, EliminarDocumento>();
            services.AddScoped<IObtenerCatalogosDocumentoService, ObtenerCatalogosDocumento>();
            return services;
        }
    }
}
