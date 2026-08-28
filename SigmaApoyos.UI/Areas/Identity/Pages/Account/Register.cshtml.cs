using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SigmaApoyos.Application.Interfaces.Services.Correo.INotificarCoordinadorService;
using SigmaApoyos.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;

namespace SigmaApoyos.UI.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly INotificarCoordinadorService _notificarCoordinadorService;
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        INotificarCoordinadorService notificarCoordinadorService,
        ILogger<RegisterModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _notificarCoordinadorService = notificarCoordinadorService;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IList<SelectListItem> Roles { get; set; } = new List<SelectListItem>();

    public string? ReturnUrl { get; set; }

    public class InputModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Display(Name = "Primer apellido")]
        public string PrimerApellido { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Display(Name = "Segundo apellido")]
        public string SegundoApellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos {2} y máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Rol")]
        public string RoleName { get; set; } = string.Empty;
    }

    public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
    {
        if (!PuedeAccederARegistro())
        {
            return Forbid();
        }

        ReturnUrl = returnUrl;
        await CargarRolesAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!PuedeAccederARegistro())
        {
            return Forbid();
        }

        returnUrl ??= Url.Content("~/");
        await CargarRolesAsync();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = new ApplicationUser
        {
            UserName = Input.Email,
            Email = Input.Email,
            EmailConfirmed = true,
            Nombre = Input.Nombre,
            PrimerApellido = Input.PrimerApellido,
            SegundoApellido = Input.SegundoApellido,
            FechaCreacion = DateTime.UtcNow,
            IdEstado = 2
        };

        var result = await _userManager.CreateAsync(user, Input.Password);
        if (result.Succeeded)
        {
            _logger.LogInformation("Usuario registrado correctamente.");

            var esPrimerUsuario = !_userManager.Users.Any(x => x.Id != user.Id);
            var rolAsignado = esPrimerUsuario ? IdentityRoles.CoordinadorAcademico : Input.RoleName;

            if (await _roleManager.RoleExistsAsync(rolAsignado))
            {
                await _userManager.AddToRoleAsync(user, rolAsignado);
            }

            await _notificarCoordinadorService.NotificarNuevoUsuarioAsync(
                $"{user.Nombre} {user.PrimerApellido} {user.SegundoApellido}".Trim(),
                user.Email ?? string.Empty,
                rolAsignado);

            if (User.Identity?.IsAuthenticated == true
                && (User.IsInRole(IdentityRoles.CoordinadorAcademico) || User.IsInRole(IdentityRoles.LegacyAdministrador)))
            {
                TempData["SuccessMessage"] = "Usuario creado correctamente.";
                return LocalRedirect("~/");
            }

            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }

    private async Task CargarRolesAsync()
    {
        if (!_userManager.Users.Any())
        {
            Roles = new List<SelectListItem>
            {
                new()
                {
                    Value = IdentityRoles.CoordinadorAcademico,
                    Text = IdentityRoles.CoordinadorAcademico
                }
            };

            Input.RoleName = IdentityRoles.CoordinadorAcademico;
            return;
        }

        var rolesDelSistema = await Task.FromResult(_roleManager.Roles
            .OrderBy(x => x.Name)
            .ToList());

        Roles = rolesDelSistema
            .Where(x => x.Name != null && IdentityRoles.EsRolVisible(x.Name))
            .Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            })
            .ToList();
    }

    private bool PuedeAccederARegistro()
    {
        var existenUsuarios = _userManager.Users.Any();

        if (!existenUsuarios)
        {
            return true;
        }

        return User.Identity?.IsAuthenticated == true && (User.IsInRole(IdentityRoles.CoordinadorAcademico) || User.IsInRole(IdentityRoles.LegacyAdministrador));
    }
}
