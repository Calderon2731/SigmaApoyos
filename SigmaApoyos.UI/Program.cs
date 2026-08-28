using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using SigmaApoyos.Infrastructure;
using SigmaApoyos.Infrastructure.Correos;
using SigmaApoyos.Infrastructure.Identity;
using SigmaApoyos.Infrastructure.Identity.Seed;
using SigmaApoyos.Infrastructure.Persistence;
using SigmaApoyos.Application;
using SigmaApoyos.Application.DTOs.Auditorias;
using SigmaApoyos.Application.Interfaces.Services.Auditoria.IRegistrarAuditoriaService;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddErrorDescriber<SpanishIdentityErrorDescriber>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IEmailSender<ApplicationUser>, IdentityEmailSender>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/AccessDenied";
    options.Events.OnSigningIn = async context =>
    {
        var auditoriaService = context.HttpContext.RequestServices.GetRequiredService<IRegistrarAuditoriaService>();
        await auditoriaService.RegistrarAsync(new RegistrarAuditoriaDto
        {
            UsuarioId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier),
            UsuarioNombre = context.Principal?.Identity?.Name ?? "Sistema",
            Accion = "Iniciar sesión",
            Entidad = "Seguridad",
            RegistroId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
            DireccionIp = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
            Ruta = context.HttpContext.Request.Path.Value,
            Descripcion = "Inicio de sesión en Sigma Apoyos"
        });
    };
    options.Events.OnSigningOut = async context =>
    {
        var auditoriaService = context.HttpContext.RequestServices.GetRequiredService<IRegistrarAuditoriaService>();
        await auditoriaService.RegistrarAsync(new RegistrarAuditoriaDto
        {
            UsuarioId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
            UsuarioNombre = context.HttpContext.User.Identity?.Name ?? "Sistema",
            Accion = "Cerrar sesión",
            Entidad = "Seguridad",
            RegistroId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
            DireccionIp = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
            Ruta = context.HttpContext.Request.Path.Value,
            Descripcion = "Cierre de sesión en Sigma Apoyos"
        });
    };
    options.Events.OnValidatePrincipal = async context =>
    {
        if (context.Principal == null)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return;
        }

        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.GetUserAsync(context.Principal);

        if (user == null || user.IdEstado != 2)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        }
    };
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await IdentityRoleSeeder.SeedAsync(roleManager);
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
